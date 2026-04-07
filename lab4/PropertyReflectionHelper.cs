using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace lab4
{
    /// <summary>
    /// Lists all public instance properties (name, type, value) in a TreeView via reflection.
    /// </summary>
    public static class PropertyReflectionHelper
    {
        public static void FillTreeWithProperties(TreeView treeView, object obj)
        {
            if (treeView == null)
                throw new ArgumentNullException(nameof(treeView));
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            treeView.Nodes.Clear();
            Type t = obj.GetType();
            TreeNode root = new TreeNode(t.Name);
            treeView.Nodes.Add(root);

            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;
            foreach (PropertyInfo pi in t.GetProperties(flags))
            {
                if (!pi.CanRead)
                    continue;

                object rawValue;
                try
                {
                    rawValue = pi.GetValue(obj, null);
                }
                catch (Exception ex)
                {
                    rawValue = "(" + ex.GetType().Name + ": " + ex.Message + ")";
                }

                string valueText = FormatValue(rawValue);
                string nodeText = string.Format("{0}  |  type: {1}  |  value: {2}",
                    pi.Name,
                    FormatType(pi.PropertyType),
                    valueText);
                root.Nodes.Add(new TreeNode(nodeText));
            }

            treeView.ExpandAll();
        }

        private static string FormatType(Type type)
        {
            if (type.IsGenericType)
            {
                var def = type.GetGenericTypeDefinition();
                var args = type.GetGenericArguments();
                var sb = new StringBuilder();
                sb.Append(def.Name.Substring(0, def.Name.IndexOf('`')));
                sb.Append("<");
                for (int i = 0; i < args.Length; i++)
                {
                    if (i > 0) sb.Append(", ");
                    sb.Append(args[i].Name);
                }
                sb.Append(">");
                return sb.ToString();
            }
            return type.Name;
        }

        private static string FormatValue(object value)
        {
            if (value == null)
                return "null";

            if (value is string s)
                return "\"" + s + "\"";

            if (value is IEnumerable enumerable && !(value is string))
            {
                var sb = new StringBuilder("[");
                bool first = true;
                foreach (object item in enumerable)
                {
                    if (!first) sb.Append(", ");
                    first = false;
                    sb.Append(item == null ? "null" : item.ToString());
                }
                sb.Append("]");
                return sb.ToString();
            }

            return value.ToString();
        }
    }
}
