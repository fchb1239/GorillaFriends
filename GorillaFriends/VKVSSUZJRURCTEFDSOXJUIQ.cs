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
            HttpClient client = new HttpClient();
            string result = await client.GetStringAsync(VR);
            using (StringReader reader = new StringReader(result))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Main.m_listVerifiedUserIds.Add(line);
                }
            }
        }
        
        async public static void QKXBQOTMSVNURUQ()
        {
            HttpClient client = new HttpClient();
            string result = await client.GetStringAsync(BL);
            using (StringReader reader = new StringReader(result))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Main.m_listVerifiedUserIds.Add(line);
                }
            }
        }
    }
}
