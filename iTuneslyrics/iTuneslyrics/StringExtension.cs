using System;
using System.Collections.Generic;

namespace iTuneslyrics {
    public static class StringExtension {
        public static string[] Lines(this String s) {
            const char newLineChar = '\n';
            const char returnChar = '\r';

            int num2;
            var list = new List<string>();

            for (int i = 0; i < s.Length; i = num2) {
                num2 = i;
                while (num2 < s.Length) {
                    char ch = s[num2];
                    if ((ch == returnChar) || (ch == newLineChar)) {
                        break;
                    }
                    num2++;
                }
                string str2 = s.Substring(i, num2 - i);
                list.Add(str2);
                if ((num2 < s.Length) && (s[num2] == returnChar)) {
                    num2++;
                }
                if ((num2 < s.Length) && (s[num2] == newLineChar)) {
                    num2++;
                }
            }
            if ((s.Length > 0) && ((s[s.Length - 1] == returnChar) || (s[s.Length - 1] == newLineChar))) {
                list.Add(String.Empty);
            }
            return list.ToArray();
        }

    }
}