using ConsignmentShopLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsignementShop
{
    public partial class ConsignmentShop : Form
    {
        private Store store = new Store();
        private List<Item> shoppingCartData = new List<Item>();
        BindingSource itemsBinding = new BindingSource();
        BindingSource cartBinding = new BindingSource();
        BindingSource vendorsBinding = new BindingSource();
        private decimal storeProfit = 0;

        public ConsignmentShop()
        {
            InitializeComponent();
            SetupData();

            //Faz com que a informação dos items passe para a lista store items
            //Para cada item que foi vendido mudamos o nome para "x".
            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            itemsListbox.DataSource = itemsBinding;

            itemsListbox.DisplayMember = "Display";
            itemsListbox.ValueMember = "Display";

            //Faz com que a informação do item selecionado passe para a lista shopping cart
            cartBinding.DataSource = shoppingCartData;
            shoppingCartListbox.DataSource = cartBinding;

            shoppingCartListbox.DisplayMember = "Display";
            shoppingCartListbox.ValueMember = "Display";

            vendorsBinding.DataSource = store.Vendors;
            vendorListbox.DataSource = vendorsBinding;

            vendorListbox.DisplayMember = "Display";
            vendorListbox.ValueMember = "Display";

        }

        //Informação para exemplificar
        private void SetupData()
        {
            //Vendedores
            store.Vendors.Add(new Vendor { FirstName = "Nuno", LastName = "Barão" });
            store.Vendors.Add(new Vendor { FirstName = "André", LastName = "Correia" });

            //Items
            store.Items.Add(new Item { Title = "Moby Dick", Description = "Livro sobre uma baleia", Price = 4.50M, Owner = store.Vendors[0] });
            store.Items.Add(new Item { Title = "A tale of two cities", Description = "Livro sobre revolução", Price = 3.80M, Owner = store.Vendors[1] });
            store.Items.Add(new Item { Title = "Harry Potter Book 1", Description = "Livro sobre um rapaz", Price = 5.20M, Owner = store.Vendors[1] });
            store.Items.Add(new Item { Title = "Jane Eyre", Description = "Livro sobre uma rapariga", Price = 1.50M, Owner = store.Vendors[0] });

            //Nome da loja
            store.Name = "Seconds are better";
        }

        private void addToCart_Click(object sender, EventArgs e)
        {
            //Descobre qual dos items desta lista está selecionado
            //copia esse item para a lista shopping cart
            Item selectedItem = (Item)itemsListbox.SelectedItem;

            shoppingCartData.Add(selectedItem);

            cartBinding.ResetBindings(false);
        }

        private void makePurchase_Click(object sender, EventArgs e)
        {
            //Marca cada item do carrinho como vendido
            //Limpa o carrinho

            foreach(Item item in shoppingCartData)
            {
                item.Sold = true;
                item.Owner.PaymentDue += (decimal)item.Owner.Commission * item.Price;
                storeProfit += (1 - (decimal)item.Owner.Commission) * item.Price;
            }

            shoppingCartData.Clear();

            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();

            storeProfitValue.Text = string.Format("{0}€", storeProfit.ToString("0.00"));

            cartBinding.ResetBindings(false);
            itemsBinding.ResetBindings(false);
            vendorsBinding.ResetBindings(false);
        }
    }
}
