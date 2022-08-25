using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class ColorUtils
    {
        private static float ResultB;
        private static float ResultG;
        private static float ResultR;
        private static float ResultMixedB;
        private static float ResultMixedG;
        private static float ResultMixedR;
        private static float ResultContextB;
        private static float ResultContextG;
        private static float ResultContextR;
        public static float ResultColor(Color ContextMixed, Color Context)
        {
            var contextMixed = ContextMixed;
            var context = Context;
            var mixedB = contextMixed.b;
            var mixedG = contextMixed.g;
            var mixedR = contextMixed.r;
            var contextB = context.b;
            var contextG = context.g;
            var contextR = context.r;
            ResultMixedB = (mixedB / 2) * 100;
            ResultMixedG = (mixedG / 2) * 100;
            ResultMixedR = (mixedR / 2) * 100;
            ResultContextB = (contextB / 2) * 100;
            ResultContextG= (contextG / 2) * 100;
            ResultContextR = (contextR / 2) * 100;
            if (ResultMixedB >= ResultContextB)
            {
                ResultB = ResultMixedB - ResultContextB;
            }
            else if (ResultMixedB < ResultContextB)
            {
                ResultB = ResultContextB - ResultMixedB;
            }
            if (ResultMixedG >= ResultContextG)
            {
                ResultG = ResultMixedG - ResultContextG;
            }
            else if (ResultMixedG < ResultContextG)
            {
                ResultG = ResultContextG - ResultMixedG;
            }
            if (ResultMixedR >= ResultContextR)
            {
                ResultR = ResultMixedR - ResultContextR;
            }
            else if (ResultMixedR < ResultContextR)
            {
                ResultR = ResultContextR - ResultMixedR;
            }
            var result = ResultB + ResultG + ResultR;
            return result;
        }
    }
}
