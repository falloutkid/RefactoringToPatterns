﻿using System;
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
            TagNode ordersTag = new TagNode("orders");
            foreach (Order order in orders.OrderList)
            {
                TagNode orderTag = new TagNode("order");
                orderTag.addAttribute("id", order.OrderId);
                writeProductsTo(orderTag, order);
                ordersTag.add(orderTag);
            }
            xml.Append(ordersTag.toString());
        }

        private void writeProductsTo(TagNode orderTag, Order order)
        {
            foreach (Product product in order.ProductList)
            {
                TagNode productTag = new TagNode("product");
                productTag.addAttribute("id", product.ID);
                productTag.addAttribute("color", colorFor(product));
                if (product.Size != ProductSize.NOT_APPLICABLE)
                    productTag.addAttribute("size", sizeFor(product));
                writePriceTo(productTag, product);
                productTag.addValue(product.Name);
                orderTag.add(productTag);
            }
        }

        private void writePriceTo(TagNode productTag, Product product)
        {
            TagNode priceTag = new TagNode("price");
            priceTag.addAttribute("currency", currencyFor(product));
            priceTag.addValue(priceFor(product));
            productTag.add(priceTag);
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

        private List<TagNode> children;

        public TagNode(String name)
        {
            this.name = name;
            attributes = new StringBuilder();
            attributes.Clear();
            children = new List<TagNode>();
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
            result = "<" + name + attributes + ">";

            foreach(TagNode node in children)
            {
                result += node.toString();
            }

            result += value;
            result += "</" + name + ">";
            return result;
        }

        public void add(TagNode tagNode)
        {
            children.Add(tagNode);
        }
    }
}
