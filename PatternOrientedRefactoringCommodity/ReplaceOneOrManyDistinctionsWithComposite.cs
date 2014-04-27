
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoringCommodity
{
    public class ProductRepository
    {
        List<Product> product_lists;

        public ProductRepository()
        {
            product_lists = new List<Product>();
        }

        public void add(Product product)
        {
            product_lists.Add(product);
        }

        public List<Product> selectBy(ColorSpec colorSpec)
        {
            List<Spec> specs = new List<Spec>();
            specs.Add(colorSpec);

            return selectBy(specs);
        }

        public List<Product> selectBy(List<Spec> specs)
        {
            CompositeSpec composite_spec = new CompositeSpec(specs);
            List<Product> return_list = new List<Product>();

            foreach (Product product in product_lists)
            {
                bool is_match = true;
                foreach (Spec spec in composite_spec.Specs)
                {
                    if (spec.GetType() == typeof(ColorSpec))
                    {
                        if (spec.SpecValue != product.Color)
                            is_match = false;
                    }
                    else if (spec.GetType() == typeof(SizeSpec))
                    {
                        if (spec.SpecValue != product.Size)
                            is_match = false;
                    }
                    else if (spec.GetType() == typeof(BelowPriceSpec))
                    {
                        if (spec.Price != product.Price)
                            is_match = false;
                    }
                }
                if (is_match)
                    return_list.Add(product);
            }

            return return_list;
        }
    }

    public class Product
    {
        private string id_;
        private string name_;
        private string color_;
        private float price_;
        private string size_;

        public string ID { get { return id_; } }
        public string Name { get { return name_; } }
        public string Color { get { return color_; } }
        public float Price { get { return price_; } }
        public string Size { get { return size_; } }

        public Product(string id, string name, string color, float price, string size)
        {
            this.id_ = id;
            this.name_ = name;
            this.color_ = color;
            this.price_ = price;
            this.size_ = size;
        }
    }

    public class Color
    {
        public static readonly string red = "red";
        public static readonly string yellow = "yellow";
        public static readonly string pink = "pink";
        public static readonly string white = "white";
    }

    public class ProductSize
    {
        public static readonly string MEDIUM = "MEDIUM";
        public static readonly string SMALL = "SMALL";
        public static readonly string LARGE = "LARGE";
        public static readonly string NOT_APPLICABLE = "NOT_APPLICABLE";
    }

    #region Spec一覧
    public class CompositeSpec
    {
        private List<Spec> specs_;
        public List<Spec> Specs { get { return specs_; } }
        public CompositeSpec(List<Spec> specs)
        {
            specs_ = specs;
        }
    }

    public class Spec
    {
        protected string spec = "";
        protected float price = 0.0F;
        public string SpecValue { get { return spec; } }
        public float Price { get { return price; } }
    }

    public class ColorSpec : Spec
    {
        public string Color { get { return spec; } }

        public ColorSpec(string color)
        {
            this.spec = color;
        }
    }

    public class SizeSpec : Spec
    {
        public string Size { get { return spec; } }

        public SizeSpec(string size)
        {
            this.spec = size;
        }
    }

    public class BelowPriceSpec : Spec
    {
        public BelowPriceSpec(float price)
        {
            this.price = price;
        }
    }
    #endregion
}
