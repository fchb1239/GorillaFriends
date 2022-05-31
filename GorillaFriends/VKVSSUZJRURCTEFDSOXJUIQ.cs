using System.IO;
using System.Net.Http;

namespace GorillaFriends
{
    class VKVSSUZJRURCTEFDSOXJUIQ
    {
        public const string VR = "https://github.com/fchb1239/GorillaFriends/raw/main/VKVSSUZJRUQ";
        public const string BL = "https://github.com/fchb1239/GorillaFriends/raw/main/QKXBQOTMSVNURUQ";
        
        async public static void VKVSSUZJRUQ()
        {
            HttpClient YSXPZWSO = new HttpClient();
            string CMVZDWX = await YSXPZWSO.GetStringAsync(VR);
            using (StringReader CMVHZGVY = new StringReader(CMVZDWX))
            {
                string BGLUZQ;
                while ((BGLUZQ = CMVHZGVY.ReadLine()) != null)
                {
                    TWFPBG.BVOSAXNOVMVYAWZPZWRVC2VYSWRZ.Add(BGLUZQ);
                }
            }
        }
        
        async public static void QKXBQOTMSVNURUQ()
        {
            HttpClient YSXPZWSO = new HttpClient();
            string CMVZDWX = await YSXPZWSO.GetStringAsync(BL);
            using (StringReader CMVHZGVY = new StringReader(CMVZDWX))
            {
                string BGLUZQ;
                while ((BGLUZQ = CMVHZGVY.ReadLine()) != null)
                {
                    TWFPBG.VUSGUKLFTKRBQKXF.Add(BGLUZQ);
                }
            }
        }
    }
}
