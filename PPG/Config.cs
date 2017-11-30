﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPG
{
    public class Config
    {
        public Config() { }

        public Dictionary<string, string> ElGamalKey = new Dictionary<string, string>
        {
            {"p", "20233811201078862036693059962506880020772594261142385675900086551707159458556547939349988513541549988398130927482259851392176019193666511024373541934184074898001997160121161152289427471412088172670899217321650838610044437726097876602074914640501363854811447493611300469290053641328737298762938740557087694885523112719763871739802059928183321922620163609337830463026200849009898050850584418866224314864674552334857460077445719998386476058150008001670923109374875007369063388879822453182473404018867898490586773864872807393531583787764231170449052691998449723596312726923803965577924581742779027327548387070576731234483" },
            {"g", "86963041094115424628346138945518598058779348481926534337952335651982726817289000494648895325522057896799104323192159896912396113044597263051928137152384493845397663913800221415844361305321585482222537802235545326575551462285123184921383866620860488285442906938792231303908150734988258736978329260570503717347679604423138474536270418574957055060564347811009780380750633231536794093687533917682309193899162138012969774685463455747560737481009512025935160353066225807456430407975751810934213740134827592560610485806876822003136704677874925742233573087887539054956439092203005769387563541107527203676090767023262592522" },
            {"y", "5756351759387878390716192515264905320952606082380550392892239575925466259995690701224504532536226107796158982196925361450375243877271470060032687683026614521190642193696214472372292360850359465834930619215993844069533828844824176226604360859634766142436591334042536440750156345185185495460002043586396409828849322194433870913188541175860800660705393818060647062335612490856278565065750241638159870082613651121456199325875137550959911891150624678873459064949308052066831371935481803542254569225221927013387127280331875665564637728063078682215024139082004454214493599428238433666241834449671070115767366895186278465286" },
            {"x", "10436019924617374432837445076935813152753542893984870656090821753990862059089982273370309997853752803772102261593473140056563319930714264008469805427255618190228146836186580077263193665060372183316006412885589373197029283159289165869991160970701210225932383745877102407604300561636264711943080229952385122484501482344043244895673814542050207319961748847350331059183019830507566929984494563410925845418939160306678214431056243032699543309038437630894384428013234047642546850174102075158936274229481238798584725393592169284466701610169985085356482109693113847741335119169649343323076706930543734080757242935809463601581" }
        };

        public string SPG_URL = "http://localhost:50233";

        public Object ADMIN = new
        {
            id = 6,
            username = "PakSV",
            password = "12345",
            LIK = "125125",
            role = "admin",
            isCastingDone = false
        };
    }
}
