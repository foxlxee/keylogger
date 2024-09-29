using System;

namespace ReceivingApp {
    // Класс, который конвертирует код клавиши в символ
    internal static class Converter {

        const int RUSSIAN_KEYBOARD_LAYOUT = 1049;

        public static string GetCharacterFromKey(Keys key, int layout, bool isShiftPressed) {

            if (key == Keys.SPACE) return " ";
            if (key == Keys.RETURN) return Environment.NewLine;

            if (key == Keys.LSHIFT || key == Keys.RSHIFT || key == Keys.SHIFT) {
                return string.Empty;
            }

            switch (key) {
                case Keys.KEY_1: return isShiftPressed ? "!" : "1";
                case Keys.KEY_5: return isShiftPressed ? "%" : "5";
                case Keys.KEY_8: return isShiftPressed ? "*" : "8";
                case Keys.KEY_9: return isShiftPressed ? "(" : "9";
                case Keys.KEY_0: return isShiftPressed ? ")" : "0";
                case Keys.OEM_MINUS: return isShiftPressed ? "_" : "-";
                case Keys.OEM_PLUS: return isShiftPressed ? "+" : "=";
                case Keys.NUMPAD0: return "0";
                case Keys.NUMPAD1: return "1";
                case Keys.NUMPAD2: return "2";
                case Keys.NUMPAD3: return "3";
                case Keys.NUMPAD4: return "4";
                case Keys.NUMPAD5: return "5";
                case Keys.NUMPAD6: return "6";
                case Keys.NUMPAD7: return "7";
                case Keys.NUMPAD8: return "8";
                case Keys.NUMPAD9: return "9";
                case Keys.SUBTRACT: return "-";
                case Keys.ADD: return "+";
                case Keys.MULTIPLY: return "*";
                case Keys.DIVIDE: return "/";
                case Keys.DECIMAL: return ".";
            }

            if (layout == RUSSIAN_KEYBOARD_LAYOUT) {
                switch (key) {
                    case Keys.OEM_3: return isShiftPressed ? "Ё" : "ё";
                    case Keys.KEY_1: return isShiftPressed ? "!" : "1";
                    case Keys.KEY_2: return isShiftPressed ? "\"" : "2";
                    case Keys.KEY_3: return isShiftPressed ? "№" : "3";
                    case Keys.KEY_4: return isShiftPressed ? ";" : "4";
                    case Keys.KEY_5: return isShiftPressed ? "%" : "5";
                    case Keys.KEY_6: return isShiftPressed ? ":" : "6";
                    case Keys.KEY_7: return isShiftPressed ? "?" : "7";
                    case Keys.KEY_8: return isShiftPressed ? "*" : "8";
                    case Keys.KEY_9: return isShiftPressed ? "(" : "9";
                    case Keys.KEY_0: return isShiftPressed ? ")" : "0";
                    case Keys.OEM_MINUS: return isShiftPressed ? "_" : "-";
                    case Keys.OEM_PLUS: return isShiftPressed ? "+" : "=";

                    case Keys.KEY_Q: return isShiftPressed ? "Й" : "й";
                    case Keys.KEY_W: return isShiftPressed ? "Ц" : "ц";
                    case Keys.KEY_E: return isShiftPressed ? "У" : "у";
                    case Keys.KEY_R: return isShiftPressed ? "К" : "к";
                    case Keys.KEY_T: return isShiftPressed ? "Е" : "е";
                    case Keys.KEY_Y: return isShiftPressed ? "Н" : "н";
                    case Keys.KEY_U: return isShiftPressed ? "Г" : "г";
                    case Keys.KEY_I: return isShiftPressed ? "Ш" : "ш";
                    case Keys.KEY_O: return isShiftPressed ? "Щ" : "щ";
                    case Keys.KEY_P: return isShiftPressed ? "З" : "з";
                    case Keys.OEM_4: return isShiftPressed ? "Х" : "х";
                    case Keys.OEM_6: return isShiftPressed ? "Ъ" : "Ъ";
                    case Keys.KEY_A: return isShiftPressed ? "Ф" : "ф";
                    case Keys.KEY_S: return isShiftPressed ? "Ы" : "ы";
                    case Keys.KEY_D: return isShiftPressed ? "В" : "в";
                    case Keys.KEY_F: return isShiftPressed ? "А" : "а";
                    case Keys.KEY_G: return isShiftPressed ? "П" : "п";
                    case Keys.KEY_H: return isShiftPressed ? "Р" : "р";
                    case Keys.KEY_J: return isShiftPressed ? "О" : "о";
                    case Keys.KEY_K: return isShiftPressed ? "Л" : "л";
                    case Keys.KEY_L: return isShiftPressed ? "Д" : "д";
                    case Keys.OEM_1: return isShiftPressed ? "Ж" : "ж";
                    case Keys.OEM_7: return isShiftPressed ? "Э" : "э";
                    case Keys.OEM_5: return isShiftPressed ? "/" : "\\";
                    case Keys.KEY_Z: return isShiftPressed ? "Я" : "я";
                    case Keys.KEY_X: return isShiftPressed ? "Ч" : "ч";
                    case Keys.KEY_C: return isShiftPressed ? "С" : "с";
                    case Keys.KEY_V: return isShiftPressed ? "М" : "м";
                    case Keys.KEY_B: return isShiftPressed ? "И" : "и";
                    case Keys.KEY_N: return isShiftPressed ? "Т" : "т";
                    case Keys.KEY_M: return isShiftPressed ? "Ь" : "ь";
                    case Keys.OEM_COMMA: return isShiftPressed ? "Б" : "б";
                    case Keys.OEM_PERIOD: return isShiftPressed ? "Ю" : "ю";
                    case Keys.OEM_2: return isShiftPressed ? "," : ".";
                }
            } else {
                switch (key) {
                    case Keys.OEM_3: return isShiftPressed ? "~" : "`";
                    case Keys.KEY_2: return isShiftPressed ? "@" : "2";
                    case Keys.KEY_3: return isShiftPressed ? "#" : "3";
                    case Keys.KEY_6: return isShiftPressed ? "^" : "6";
                    
                    case Keys.KEY_Q: return isShiftPressed ? "Q" : "q";
                    case Keys.KEY_W: return isShiftPressed ? "W" : "w";
                    case Keys.KEY_E: return isShiftPressed ? "E" : "e";
                    case Keys.KEY_R: return isShiftPressed ? "R" : "r";
                    case Keys.KEY_T: return isShiftPressed ? "T" : "t";
                    case Keys.KEY_Y: return isShiftPressed ? "Y" : "y";
                    case Keys.KEY_U: return isShiftPressed ? "U" : "u";
                    case Keys.KEY_I: return isShiftPressed ? "I" : "i";
                    case Keys.KEY_O: return isShiftPressed ? "O" : "o";
                    case Keys.KEY_P: return isShiftPressed ? "P" : "p";
                    case Keys.OEM_4: return isShiftPressed ? "{" : "[";
                    case Keys.OEM_6: return isShiftPressed ? "}" : "]";
                    case Keys.KEY_A: return isShiftPressed ? "A" : "a";
                    case Keys.KEY_S: return isShiftPressed ? "S" : "s";
                    case Keys.KEY_D: return isShiftPressed ? "D" : "d";
                    case Keys.KEY_F: return isShiftPressed ? "F" : "f";
                    case Keys.KEY_G: return isShiftPressed ? "G" : "g";
                    case Keys.KEY_H: return isShiftPressed ? "H" : "h";
                    case Keys.KEY_J: return isShiftPressed ? "J" : "j";
                    case Keys.KEY_K: return isShiftPressed ? "K" : "k";
                    case Keys.KEY_L: return isShiftPressed ? "L" : "l";
                    case Keys.OEM_1: return isShiftPressed ? ":" : ";";
                    case Keys.OEM_7: return isShiftPressed ? "\"" : "'";
                    case Keys.OEM_5: return isShiftPressed ? "|" : "\\";
                    case Keys.KEY_Z: return isShiftPressed ? "Z" : "z";
                    case Keys.KEY_X: return isShiftPressed ? "X" : "x";
                    case Keys.KEY_C: return isShiftPressed ? "C" : "c";
                    case Keys.KEY_V: return isShiftPressed ? "V" : "v";
                    case Keys.KEY_B: return isShiftPressed ? "B" : "b";
                    case Keys.KEY_N: return isShiftPressed ? "N" : "n";
                    case Keys.KEY_M: return isShiftPressed ? "M" : "m";
                    case Keys.OEM_COMMA: return isShiftPressed ? "<" : ",";
                    case Keys.OEM_PERIOD: return isShiftPressed ? ">" : ".";
                    case Keys.OEM_2: return isShiftPressed ? "?" : "/";
                }
            }

            return "  *" + key.ToString() + "*  ";
        }
    }
}