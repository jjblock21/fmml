using Injected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Miscellaneous
{
    public class ItemSelector<T>
    {
        private int selectedItem;
        private int maxEntries;
        private string[] itemNames;
        private bool hasFired = false;

        public ItemSelector()
        {
            selectedItem = 0;
            itemNames = Enum.GetNames(typeof(T));
            maxEntries = itemNames.Length;
        }

        public int Cycle()
        {
            selectedItem++;
            if (selectedItem > maxEntries - 1)
                selectedItem = 0;
            return selectedItem;
        }

        // When the button returns true, only cycle once and then wait until the button is released and pressed again.
        public bool UICycle(bool input)
        {
            if (input)
            {
                if (hasFired) return false;
                Cycle();
                hasFired = true;
                return true;
            }
            hasFired = false;
            return false;
        }

        public int GetSelected() => selectedItem;

        public string GetSelectedName(bool splitPascalCase = true)
        {
            string output = itemNames[selectedItem];
            if (splitPascalCase)
            {
                return Utilities.SplitPascalCase(output);
            }
            return output;
        }

        public T GetSelectedEnumEntry()
        {
            return (T)Enum.Parse(typeof(T), GetSelectedName(splitPascalCase: false), true);
        }

        public string GetDefaultElementName(bool splitPascalCase = true)
        {
            string output = Enum.GetNames(typeof(T))[0];
            if (splitPascalCase)
            {
                return Utilities.SplitPascalCase(output);
            }
            return output;
        }
    }
}