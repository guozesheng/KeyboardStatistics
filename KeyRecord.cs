using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeyboardStatistics
{
    public class KeyRecord
    {
        private int[] _key;
        private int _keyCount = 0;

        private static KeyRecord _keyRecord = null;

        public KeyRecord()
        {
            _key = new int[26];
        }

        public static KeyRecord Get()
        {
            if (_keyRecord == null)
            {
                _keyRecord = new KeyRecord();
            }

            return _keyRecord;
        }

        public void keyadd(int keycode)
        {
            if (keycode >= 65 && keycode <= 90)
            {
                _key[keycode - 65]++;
            }
            _keyCount++;
        }
    }
}
