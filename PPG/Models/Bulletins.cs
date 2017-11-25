using Newtonsoft.Json;
using Org.BouncyCastle.Math;
using PPG.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PPG.Data;

namespace PPG.Models
{
    public class Bulletins
    {
        private ElectContext electContext;
        private Config config;

        public Bulletins(ElectContext electContext, Config config)
        {
            this.electContext = electContext;
            this.config = config;
        }

        public DecryptedBulletin[] decryptBulletins (Bulletin[] bulletins)
        {
            BigInteger p = new BigInteger(config.ElGamalKey["p"]);
            BigInteger g = new BigInteger(config.ElGamalKey["g"]);
            BigInteger y = new BigInteger(config.ElGamalKey["y"]);
            BigInteger x = new BigInteger(config.ElGamalKey["x"]);
            BigInteger r = new BigInteger(RandomIntegerBelow(p.ToString()));
            //BigInteger r = new BigInteger("4563050602903359928056136975567095439379627199326955519680189688036774281410041321175819876909720647804719670824093616183296383061532044666546235933833472251673096343116272450105539664054763234915223028670551431957859529693271468403314305086392727648853573440577217245464756227189485983502306986972904963448440312549274813331835503711865774776771198609402798831076618538230001307631055581113010460275058655927320575339606013306989068574111583648443543871675889493183316705825816481846124482880599335932066798851139920051066669702619878112698138668324247383153274909065532392098208265789989366961315859898840554496025");

            DecryptedBulletin[] decryptedBulletins = new DecryptedBulletin[bulletins.Length];

            for (int i = 0; i < bulletins.Length; i++)
            {
                Bulletin bulletin = bulletins[i];
                BigInteger a = new BigInteger(bulletin.Data.a);
                BigInteger b = new BigInteger(bulletin.Data.b);
                BigInteger aBlind = g.ModPow(r, p).Multiply(a).Remainder(p);
                BigInteger ax = aBlind.ModPow(x, p);
                BigInteger plaintextBlind = ax.ModInverse(p).Multiply(b).Remainder(p);
                BigInteger plainText = y.ModPow(r, p).Multiply(plaintextBlind).Remainder(p);
                var str = System.Text.Encoding.Default.GetString(plainText.ToByteArray());
                DecryptedBulletin decryptedBulletin = new DecryptedBulletin();
                decryptedBulletin.votes = JsonConvert.DeserializeObject<List<DecryptedVote>>(str);
                decryptedBulletins[i] = decryptedBulletin;
            }
            return decryptedBulletins;
        }

        public void countBulletins(DecryptedBulletin[] decryptedBulletins)
        {
            foreach(DecryptedBulletin decryptedBulletin in decryptedBulletins)
            {
                foreach(DecryptedVote vote in decryptedBulletin.votes)
                {
                    electContext.Votes.Add(vote);
                    electContext.SaveChanges();
                }
            }
        }

        private string RandomIntegerBelow(string Nstr)
        {
            System.Numerics.BigInteger N = System.Numerics.BigInteger.Parse(Nstr);
            byte[] bytes = N.ToByteArray();
            System.Numerics.BigInteger R;
            var random = new Random();
            do
            {
                random.NextBytes(bytes);
                bytes[bytes.Length - 1] &= (byte)0x7F;
                R = new System.Numerics.BigInteger (bytes);
            } while (R >= N);
            return R.ToString();
        }
    }
}
