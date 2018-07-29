using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGData {
    public static class Util {
        public static int GCD(int a, int b) {
            if (a == b) return a;
            if (a == 1 || b == 1) return 1;
            if (a == 0) return b;
            if (b == 0) return a;
            if (a % b == 0) return b;
            return GCD(b, a % b);
        }
        public static ulong GCD(ulong a, ulong b) {
            if (a == b) return a;
            if (a == 1 || b == 1) return 1;
            if (a == 0) return b;
            if (b == 0) return a;
            if (a % b == 0) return b;
            return GCD(b, a % b);
        }
        public static int LCM(int a, int b) {
            if (a == b) return a;
            return a / GCD(a, b) * b;
        }
        public static ulong LCM(ulong a, ulong b) {
            if (a == b) return a;
            return a / GCD(a, b) * b;
        }
    }
}
