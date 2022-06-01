using System;
using System.Collections.Generic;
using System.Text;

namespace mini_omega
{
    class Menu
    {
        private string caption { get; }
        private List<MenuItem> menuItems = new List<MenuItem>();


        public Menu(string caption)
        {
            this.caption = caption;
        }

        public void Show()
        {
            Console.WriteLine(caption);

            foreach (var item in menuItems)
            {
                Console.WriteLine(item);
            }
        }

        public MenuItem Selection()
        {
            string input = Console.ReadLine();
            int idx;
            if (!int.TryParse(input, out idx))
            {
                Console.Error.WriteLine($"Invalid input '{input}', please enter an intexer");
                return null;
            }

            
            return menuItems[idx-1];
        }

        public MenuItem Execute()
        {
            MenuItem item = null;

            do
            {
                Show();
                item = Selection();

            } while (item == null);
            return item;

        }

        public void Add(MenuItem menuItem)
        {
            menuItems.Add(menuItem);
        }
    

}
}
