using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace NFC.Util
{
    public class ControlUtil
    {
        public static int? TryParseInt(string text)
        {
            return ParseUtil.TryParseInt(text);
        }

        public static decimal? TryParseDecimal(string text)
        {
            return ParseUtil.TryParseDecimal(text);
        }

        public static bool? TryParseBool(string text)
        {
            return ParseUtil.TryParseBool(text);
        }

        public static int? GetSelectedValue(DropDownList combo)
        {
            int? id = TryParseInt(combo.SelectedValue);
            return id == 0 ? null : id;
        }

        public static int? GetSelectedValue(HtmlSelect combo)
        {
            if (combo.SelectedIndex < 0) return null;

            int? id = TryParseInt(combo.Value);
            return id == 0 ? null : id;
        }


        public static void SelectRadio(List<RadioButton> btns, List<int> values, int value)
        {
            int index = 0;

            for (int i = 0; i < values.Count; i++)
            {
                if (values[i] == value)
                {
                    index = i;
                    break;
                }
            }
            if (index >= btns.Count) index = 0;
            if (index < btns.Count) btns[index].Checked = true;
        }

        public static void SelectRadio(List<RadioButton> btns, int value, int startValue)
        {
            int index = value - startValue;
            if (index < 0) index = 0;
            if (index >= btns.Count) index = 0;
            if (index < btns.Count) btns[index].Checked = true;
        }

        public static int? GetTextInt(TextBox box)
        {
            return ParseUtil.TryParseInt(box.Text);
        }

        public static decimal? GetTextDecimal(TextBox box, int round = 0)
        {
            return ParseUtil.TryParseDecimal(box.Text, round);
        }

        public static DateTime? GetTextDate(TextBox box, string format = "dd/MM/yyyy")
        {
            return DateUtil.TryParseDate(box.Text, format);
        }

        public static void DataBindRadioList(RadioButtonList combo, object dataSource, string valueField, string textField,
            object unassignedValue = null, string unassignedText = null)
        {
            try
            {
                //combo.Items.Clear();
                combo.DataSource = null;
                combo.DataSource = dataSource;
                combo.DataValueField = valueField;
                combo.DataTextField = textField;
                combo.DataBind();
                combo.SelectedIndex = 1;
            }
            catch (Exception e)
            {
                var ms = e.Message;
            }
            if (unassignedValue != null && unassignedText != null)
            {
                combo.Items.Insert(0, new ListItem(unassignedText, unassignedValue.ToString()));
            }
        }


        public static void DataBind(DropDownList combo, object dataSource, string valueField, string textField,
            object unassignedValue = null, string unassignedText = null)
        {
            try
            {
                //combo.Items.Clear();
                combo.DataSource = null;
                combo.DataSource = dataSource;
                combo.DataValueField = valueField;
                combo.DataTextField = textField;
                combo.DataBind();
            }
            catch (Exception e)
            {
                var ms = e.Message;
            }
            if (unassignedValue != null && unassignedText != null)
            {
                combo.Items.Insert(0, new ListItem(unassignedText, unassignedValue.ToString()));
            }
        }

        internal static void DataBind(ListBox listbox, object dataSource, string valueField, string textField, string unassignedValue = null, string unassignedText = null)
        {
            listbox.DataSource = dataSource;
            listbox.DataTextField = textField;
            listbox.DataValueField = valueField;
            listbox.DataBind();

            if (unassignedValue != null && unassignedText != null)
            {
                listbox.Items.Insert(0, new ListItem(unassignedText, unassignedValue));
            }
        }

        internal static void DataBindOpen(ListBox listbox, object dataSource, string valueField, string textField, string unassignedValue = null, string unassignedText = null)
        {
            listbox.Items.Insert(1, new ListItem(unassignedText, unassignedValue));
            listbox.Items.Insert(2, new ListItem(unassignedText, unassignedValue));
            listbox.Items.Insert(0, new ListItem(unassignedText, unassignedValue));
        }

        public static void DataBindAndSelect(DropDownList combo, object dataSource, string valueField, string textField,
            object unassignedValue, string unassignedText, object selectedValue, object value = null, string text = null)
        {
            DataBind(combo, dataSource, valueField, textField, unassignedValue, unassignedText);
            SelectValue(combo, selectedValue, value, text);
        }

        public static void DataBindAndSelect(DropDownList combo, object dataSource, string valueField, string textField,
            object selectedValue, object value = null, string text = null)
        {
            DataBind(combo, dataSource, valueField, textField);
            SelectValue(combo, selectedValue, value, text);
        }

        public static void DataBind(HtmlSelect combo, object dataSource, string valueField, string textField,
            object unassignedValue = null, string unassignedText = null)
        {
            combo.Items.Clear();
            combo.DataSource = dataSource;
            combo.DataValueField = valueField;
            combo.DataTextField = textField;
            combo.DataBind();
            if (unassignedValue != null && unassignedText != null)
            {
                combo.Items.Insert(0, new ListItem(unassignedText, unassignedValue.ToString()));
            }
        }
        public static void DataBindAndSelect(HtmlSelect combo, object dataSource, string valueField, string textField,
            object unassignedValue, string unassignedText, object selectedValue, object value = null, string text = null)
        {
            DataBind(combo, dataSource, valueField, textField, unassignedValue, unassignedText);
            SelectValue(combo, selectedValue, value, text);
        }
        public static void DataBindAndSelect(HtmlSelect combo, object dataSource, string valueField, string textField,
            object selectedValue, object value = null, string text = null)
        {
            DataBind(combo, dataSource, valueField, textField);
            SelectValue(combo, selectedValue, value, text);
        }

        public static void SelectValue(DropDownList combo, object selectedValue, object value = null, string text = null)
        {
            string selValue = selectedValue == null ? "0" :
                (selectedValue is string ? (string)selectedValue : selectedValue.ToString());

            try
            {
                combo.SelectedValue = selValue;
            }
            catch (Exception e)
            {
                //log.Error(e, "Error", null);
            }

            if (combo.SelectedValue != selValue && value != null && text != null)
            {
                combo.Items.Add(new ListItem(text, value.ToString()));
                SelectValue(combo, selectedValue);
            }
        }
        public static void SelectValueByText(DropDownList combo, string text, string value = "0")
        {
            foreach (ListItem item in combo.Items)
            {
                if (item.Text == text)
                {
                    combo.SelectedValue = item.Value;
                    return;
                }

            }
            ListItem newItem = new ListItem() { Text = text, Value = "0" };

            combo.Items.Add(newItem);
            combo.SelectedValue = newItem.Value;

        }

        public static void SelectValue(HtmlSelect combo, object selectedValue, object value = null, string text = null)
        {
            string selValue = selectedValue == null ? "0" :
                (selectedValue is string ? (string)selectedValue : selectedValue.ToString());

            bool selected = false;
            foreach (ListItem item in combo.Items)
            {
                if (item.Value == selValue)
                {
                    selected = true;
                    item.Selected = true;
                }
            }
            if (!selected && value != null && text != null)
            {
                combo.Items.Add(new ListItem(text, value.ToString()));
                SelectValue(combo, selectedValue);
            }
        }

        public static void SelectValue(HtmlSelect combo, List<object> selectedValue)
        {
            foreach (var value in selectedValue)
            {
                SelectValue(combo, value);
            }
        }

        public static Control FindControl(string controlId, Control parent)
        {
            Control result = parent.FindControl(controlId);
            if (result != null) return result;
            foreach (Control control in parent.Controls)
            {
                result = FindControl(controlId, control);
                if (result != null) return result;
            }
            return null;
        }
    }
}