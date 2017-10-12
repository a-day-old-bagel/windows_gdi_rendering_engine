using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids3D
{
    class ToggleKeyRepeatPreventer
    {
        Dictionary<char, Wrapper<int>> recentPresses;

        public ToggleKeyRepeatPreventer ()
        {
            recentPresses = new Dictionary<char, Wrapper<int>>();
        }

        public void registerNewToggleKey (char key)
        {
            recentPresses.Add(key, new Wrapper<int>(0));
        }

        public void advanceCounters()
        {
            foreach (KeyValuePair<char, Wrapper<int>> kvp in recentPresses)
            {
                if (recentPresses[kvp.Key].Value > 0)
                    recentPresses[kvp.Key].Value--;
            }
        }

        public void pressAttempt(char key, Action action)
        {
            try
            {
                if (recentPresses[key].Value <= 0)
                {
                    recentPresses[key].Value = 60;
                    action();
                }
            }
            catch { MessageBox.Show("Key not registered in repeat preventer!"); }
        }
    }
}
