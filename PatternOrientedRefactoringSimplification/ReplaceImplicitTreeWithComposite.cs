using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoringSimplification
{
    public class ProductSize
    {
        public static readonly Int16 NOT_APPLICABLE = 0;
        public static readonly Int16 APPLICABLE = 1;
    }

    public class Product
    {
        public string ID { get; set; }
        public Int16 Size { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
    }
    public class Order
    {
        public string OrderId { set; get; }
        List<Product> product_list = new List<Product>();
        public List<Product> ProductList { get { return product_list; } }
    }
    public class Orders
    {
        List<Order> order_list = new List<Order>();
        public List<Order> OrderList { get { return order_list; } }
    }

    public class OrdersWriter
    {
        private Orders orders;

        public OrdersWriter(Orders orders)
        {
            this.orders = orders;
        }

        public String getContents()
        {
            StringBuilder xml = new StringBuilder();
            writeOrderTo(xml);
            return xml.ToString();
        }

        private void writeOrderTo(StringBuilder xml)
        {
            xml.Append("<orders>");
            foreach (Order order in orders.OrderList)
            {
                xml.Append("<order");
                xml.Append(" id='");
                xml.Append(order.OrderId);
                xml.Append("'>");
                writeProductsTo(xml, order);
                xml.Append("</order>");
            }
            xml.Append("</orders>");
        }

        private void writeProductsTo(StringBuilder xml, Order order)
        {
            foreach (Product product in order.ProductList)
            {
                xml.Append("<product");
                xml.Append(" id='");
                xml.Append(product.ID);
                xml.Append("'");
                xml.Append(" color='");
                xml.Append(colorFor(product));
                xml.Append("'");
                if (product.Size != ProductSize.NOT_APPLICABLE)
                {
                    xml.Append(" size='");
                    xml.Append(sizeFor(product));
                    xml.Append("'");
                }
                xml.Append(">");
                writePriceTo(xml, product);
                xml.Append(product.Name);
                xml.Append("</product>");
            }
        }

        private string sizeFor(Product product)
        {
            throw new NotImplementedException();
        }

        private string colorFor(Product product)
        {
            throw new NotImplementedException();
        }

        private void writePriceTo(StringBuilder xml, Product product)
        {
            TagNode priceNode = new TagNode("price");
            priceNode.addAttribute("currency", currencyFor(product));
            priceNode.addValue(priceFor(product));
            xml.Append(priceNode.toString());
        }

        private string priceFor(Product product)
        {
            throw new NotImplementedException();
        }

        private string currencyFor(Product product)
        {
            throw new NotImplementedException();
        }
    }

    public class TagNode
    {
        private String name = "";
        private String value = "";
        private StringBuilder attributes;

        public TagNode(String name)
        {
            this.name = name;
            attributes = new StringBuilder();
            attributes.Clear();
        }

        public void addAttribute(String attribute, String value)
        {
            attributes.Append(" ");
            attributes.Append(attribute);
            attributes.Append("='");
            attributes.Append(value);
            attributes.Append("'");
        }

        public void addValue(String value)
        {
            this.value = value;
        }

        public String toString()
        {
            String result;
            result =
              "<" + name + attributes + ">" +
              value +
              "</" + name + ">";
            return result;
        }
    }
}
