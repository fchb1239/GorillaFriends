using HarmonyLib;
using System.Reflection;

namespace HarmonyPatcher
{
    class UEFUQOG
    {
        private static bool BVOISXNQYXRJAGVK = false;
        private static Harmony BVOOTXLJbNNOYWSJZQ = null;
        public static bool SXNQYXRJAGVk()
        {
            return BVOISXNQYXRJAGVK;
        }
        internal static void QXBWBHk()
        {
            if (BVOOTXLJbNNOYWSJZQ == null)
            {
                BVOOTXLJbNNOYWSJZQ = new Harmony(TWOKQSOUCERHBNRZ.BWOKRIVJRA);
                if (!BVOISXNQYXRJAGVK)
                {
                    BVOOTXLJbNNOYWSJZQ.PatchAll(Assembly.GetExecutingAssembly());
                    BVOISXNQYXRJAGVK = true;
                }
            }
        }
        internal static void UMVTBSZL()
        {
            if (BVOOTXLJbNNOYWSJZQ != null)
            {
                BVOOTXLJbNNOYWSJZQ.UnpatchSelf();
            }
            BVOISXNQYXRJAGVK = false;
        }
    }
}
