using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ProductViewControl
{
    public partial class ProductView : UserControl
    {
        // Extra functionaliy not implemented. It might be for instance a filter to select random products from
        private string _searchString;
        public string SearchString
        {
            get { return _searchString; }
            set { _searchString = value; }
        }

        public event EventHandler<SizeButtonClickedEventArgs> SizeButtonClicked;

        public ProductView()
        {
            InitializeComponent();
            ShowRandomProduct();
        }

        public void ShowRandomProduct()
        {
            var ids = DataAccess.GetProductsModelIDs();
            Random rnd = new Random();
            int rndIndex = rnd.Next(0, ids.Count);
            int rndProductModelId = ids[rndIndex];
            
            var productModel = DataAccess.GetProductModel(rndProductModelId);
            
            MemoryStream ms = new MemoryStream(productModel.LargePhoto);
            Image largePhoto = Image.FromStream(ms);
            pictureBox1.Image = largePhoto;

            toolTip1.SetToolTip(pictureBox1, "ProductModelID: "+ productModel.ProductModelID +" - Click image to view another product!");

            productModelNameTextBox.Text = productModel.ProductModelID + ": " + productModel.Name;
            productPriceTextBox.Text = productModel.ListPrice.ToString("C");

            productModel.ProductSizes = DataAccess.GetProducts(rndProductModelId);

            flowLayoutPanel1.Controls.Clear();

            foreach (Product product in productModel.ProductSizes)
            {
                Button sizeButton = new Button();
                sizeButton.Text = product.ProductId + " - " + product.Size;
                sizeButton.Name = product.ProductId.ToString();
                flowLayoutPanel1.Controls.Add(sizeButton);
                sizeButton.Click += SizeButton_Click;
            }
        }

        private void SizeButton_Click(object sender, EventArgs e)
        {
            SizeButtonClickedEventArgs sizeButtonClickedEventArgs = new SizeButtonClickedEventArgs();
            sizeButtonClickedEventArgs.ProductId = int.Parse(((Button)sender).Name);
            OnSizeButtonClicked(sizeButtonClickedEventArgs);
        }


        protected virtual void OnSizeButtonClicked(SizeButtonClickedEventArgs e)
        {
            SizeButtonClicked?.Invoke(this, e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ShowRandomProduct();
        }
    }
}
