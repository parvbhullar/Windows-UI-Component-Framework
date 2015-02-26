using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{

    public class CtrlNullableComboBox : Oranikle.Studio.Controls.StyledComboBox
    {

        private bool _DisposeDataSource;
        private System.Type _ItemType;
        private bool _Nullable;
        private string _NullDisplay;
        private bool itemTypeUniqueList;

        private static System.Collections.Generic.Dictionary<System.Type,System.Collections.ArrayList> NullableMap;
        private static System.Collections.Generic.Dictionary<System.Type,object> NullItemMap;
        private static System.Collections.Generic.Dictionary<System.Type,System.Collections.ArrayList> StandardMap;

        [System.ComponentModel.DefaultValue(false)]
        public bool DisposeDataSource
        {
            get
            {
                return _DisposeDataSource;
            }
            set
            {
                _DisposeDataSource = value;
            }
        }

        public System.Type ItemType
        {
            get
            {
                return _ItemType;
            }
            set
            {
                _ItemType = value;
                RefreshDataSource();
            }
        }

        public bool ItemTypeUniqueList
        {
            get
            {
                return itemTypeUniqueList;
            }
            set
            {
                itemTypeUniqueList = value;
            }
        }

        public bool Nullable
        {
            get
            {
                return _Nullable;
            }
            set
            {
                _Nullable = value;
                RefreshDataSource();
            }
        }

        [System.ComponentModel.DefaultValue("")]
        public string NullDisplay
        {
            get
            {
                return _NullDisplay;
            }
            set
            {
                _NullDisplay = value;
                RefreshDataSource();
            }
        }

        public System.Nullable<int> SelectedNullableIntValue
        {
            get
            {
                System.Nullable<int> nullable;
                System.Nullable<int> nullable2;

                if (DesignMode)
                {
                    nullable2 = new System.Nullable<int>();
                    return nullable2;
                }
                if (!Nullable)
                    return (System.Nullable<int>)SelectedValue;
                System.Nullable<int> nullable1 = (System.Nullable<int>)(SelectedValue as System.Nullable<int>);
                if (!(nullable1.GetValueOrDefault()==0 && nullable1.HasValue))
                {
                    nullable = new System.Nullable<int>();
                    return nullable;
                }
                return (System.Nullable<int>)SelectedValue;
            }
            set
            {
                if (!Nullable)
                {
                    SelectedValue = value;
                    return;
                }
                if (!value.HasValue)
                {
                    SelectedValue = 0;
                    return;
                }
                SelectedValue = value;
            }
        }

        public CtrlNullableComboBox()
        {
            _Nullable = true;
            _NullDisplay = "";
            itemTypeUniqueList = true;
        }

        static CtrlNullableComboBox()
        {
            Oranikle.Studio.Controls.CtrlNullableComboBox.StandardMap = new System.Collections.Generic.Dictionary<System.Type,System.Collections.ArrayList>();
            Oranikle.Studio.Controls.CtrlNullableComboBox.NullableMap = new System.Collections.Generic.Dictionary<System.Type,System.Collections.ArrayList>();
            Oranikle.Studio.Controls.CtrlNullableComboBox.NullItemMap = new System.Collections.Generic.Dictionary<System.Type,object>();
        }

        private System.Collections.ArrayList AddInNullItem(System.Collections.ArrayList toBeBindedList, bool isStandardList, out bool changedList)
        {
            changedList = false;
            if (isStandardList)
            {
                if (!_Nullable && Oranikle.Studio.Controls.CtrlNullableComboBox.StandardMap.ContainsKey(_ItemType))
                    return Oranikle.Studio.Controls.CtrlNullableComboBox.StandardMap[_ItemType];
                if (_Nullable && System.String.IsNullOrEmpty(NullDisplay) && Oranikle.Studio.Controls.CtrlNullableComboBox.NullableMap.ContainsKey(_ItemType))
                    return Oranikle.Studio.Controls.CtrlNullableComboBox.NullableMap[_ItemType];
            }
            System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
            arrayList.AddRange(toBeBindedList);
            toBeBindedList = arrayList;
            if (_Nullable)
                toBeBindedList = AddNullItem(toBeBindedList, out changedList);
            if (isStandardList)
            {
                if (!_Nullable && !Oranikle.Studio.Controls.CtrlNullableComboBox.StandardMap.ContainsKey(_ItemType))
                    Oranikle.Studio.Controls.CtrlNullableComboBox.StandardMap.Add(_ItemType, toBeBindedList);
                else if (_Nullable && System.String.IsNullOrEmpty(NullDisplay) && !Oranikle.Studio.Controls.CtrlNullableComboBox.NullableMap.ContainsKey(_ItemType))
                    Oranikle.Studio.Controls.CtrlNullableComboBox.NullableMap.Add(_ItemType, toBeBindedList);
            }
            return toBeBindedList;
        }

        private System.Collections.ArrayList AddNullItem(System.Collections.ArrayList toBeBindedList, out bool changedList)
        {
            object obj = GetNullItem();
            if ((toBeBindedList.Count >= 1) && (toBeBindedList[0] == obj))
            {
                changedList = false;
                return toBeBindedList;
            }
            toBeBindedList.Insert(0, obj);
            changedList = true;
            return toBeBindedList;
        }

        private System.Type GetBoundType(object b)
        {
            if ((b is System.Windows.Forms.Binding))
                return GetBoundType(((System.Windows.Forms.Binding)b).DataSource);
            if ((b is System.Windows.Forms.BindingSource))
                return GetBoundType(((System.Windows.Forms.BindingSource)b).DataSource);
            if ((b is System.Type))
                return (System.Type)b;
            return b.GetType();
        }

        private System.Collections.ArrayList GetCurrentDataSourceAsArrayList()
        {
            System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
            foreach (object obj in (System.Collections.IEnumerable)((System.Windows.Forms.BindingSource)DataSource).DataSource)
            {
                arrayList.Add(obj);
            }
            return arrayList;
        }

        public object GetCurrentlyBoundId()
        {
            System.Windows.Forms.Binding binding = GetSelectedValueBinding();
            if (binding == null)
                return null;
            if (((System.Windows.Forms.BindingSource)binding.DataSource).Count == 0)
                return null;
            object obj = ((System.Windows.Forms.BindingSource)binding.DataSource)[0];
            System.Type type = obj.GetType();
            System.Windows.Forms.BindingMemberInfo bindingMemberInfo = binding.BindingMemberInfo;
            string s = bindingMemberInfo.BindingMember;
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(s);
            return propertyInfo.GetValue(obj, null);
        }

        private object GetNullItem()
        {
            if (Oranikle.Studio.Controls.CtrlNullableComboBox.NullItemMap.ContainsKey(_ItemType))
                return Oranikle.Studio.Controls.CtrlNullableComboBox.NullItemMap[_ItemType];
            if (_ItemType == typeof(Oranikle.Studio.Controls.EnumDisplay))
            {
                Oranikle.Studio.Controls.EnumDisplay enumDisplay = new Oranikle.Studio.Controls.EnumDisplay(0, NullDisplay, "");
                Oranikle.Studio.Controls.CtrlNullableComboBox.NullItemMap.Add(typeof(Oranikle.Studio.Controls.EnumDisplay), enumDisplay);
                return enumDisplay;
            }
            object obj = System.Activator.CreateInstance(_ItemType);
            System.Reflection.PropertyInfo propertyInfo1 = _ItemType.GetProperty(DisplayMember);
            propertyInfo1.SetValue(obj, Oranikle.Studio.Controls.Util.Translate(NullDisplay), null);
            System.Reflection.PropertyInfo propertyInfo2 = _ItemType.GetProperty(ValueMember);
            propertyInfo2.SetValue(obj, null, null);
            Oranikle.Studio.Controls.CtrlNullableComboBox.NullItemMap.Add(_ItemType, obj);
            return obj;
        }

        private System.Windows.Forms.Binding GetSelectedValueBinding()
        {
            System.Windows.Forms.Binding binding2;

            foreach (System.Windows.Forms.Binding binding1 in DataBindings)
            {
                if (binding1.PropertyName == "SelectedValue")
                {
                    return binding1;
                }
            }
            return null;
        }

        private bool GuessIfNeedToRebindList(System.Collections.ArrayList toBeBindedList)
        {
            System.Collections.ArrayList arrayList = GetCurrentDataSourceAsArrayList();
            if (toBeBindedList.Count != arrayList.Count)
                return true;
            if (_Nullable)
            {
                if (arrayList.Count <= 0)
                    return true;
                if (arrayList[0] != GetNullItem())
                    return true;
            }
            return false;
        }

        public void RefreshDataSource(bool preserveOldValue)
        {
            bool flag2;

            if ((DataSource == null) || (((System.Windows.Forms.BindingSource)DataSource).DataSource == null) || (_ItemType == null) || (DisplayMember == null) || DisplayMember == "" || (ValueMember == null) || ValueMember == "" || DataSource is System.Windows.Forms.BindingSource || ((System.Windows.Forms.BindingSource)DataSource).DataSource is System.Collections.ICollection)
                return;
            object obj = SelectedItem;
            UpdateNullBinding();
            bool flag1 = false;
            System.Collections.ArrayList arrayList = GetCurrentDataSourceAsArrayList();
            if (itemTypeUniqueList)
                flag2 = true;
            else
                flag2 = false;
            if (ItemType == typeof(Oranikle.Studio.Controls.EnumDisplay))
                flag2 = false;
            arrayList = AddInNullItem(arrayList, flag2, out flag1);
            if (GuessIfNeedToRebindList(arrayList))
                flag1 = true;
            if (!flag1)
                return;
            ((System.Windows.Forms.BindingSource)DataSource).DataSource = null;
            ((System.Windows.Forms.BindingSource)DataSource).DataSource = arrayList;
            ((System.Windows.Forms.BindingSource)DataSource).ResetBindings(false);
            if (preserveOldValue)
                SelectedItem = obj;
            foreach (System.Windows.Forms.Binding binding in DataBindings)
            {
                binding.ReadValue();
            }
            if (!preserveOldValue && _Nullable && (Items.Count > 0))
                SelectedIndex = 0;
        }

        public void RefreshDataSource()
        {
            RefreshDataSource(false);
        }

        private void UpdateNullBinding()
        {
            if (Nullable && (_ItemType != null))
            {
                System.Windows.Forms.Binding binding = GetSelectedValueBinding();
                if (binding != null)
                {
                    System.Reflection.PropertyInfo propertyInfo1 = _ItemType.GetProperty(ValueMember);
                    binding.NullValue = propertyInfo1.GetValue(GetNullItem(), null);
                    if (binding == null)
                    {
                        binding.DataSourceNullValue = binding.NullValue;
                        return;
                    }
                    System.Type type = GetBoundType(binding);
                    System.Windows.Forms.BindingMemberInfo bindingMemberInfo = binding.BindingMemberInfo;
                    string s = bindingMemberInfo.BindingMember;
                    System.Reflection.PropertyInfo propertyInfo2 = type.GetProperty(s);
                    object obj = System.Activator.CreateInstance(propertyInfo2.PropertyType);
                    binding.DataSourceNullValue = obj;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && DisposeDataSource && (DataSource is System.IDisposable))
                ((System.IDisposable)DataSource).Dispose();
            base.Dispose(disposing);
        }

        protected override void OnDataSourceChanged(System.EventArgs e)
        {
            base.OnDataSourceChanged(e);
            RefreshDataSource();
        }

        protected override void OnDisplayMemberChanged(System.EventArgs e)
        {
            base.OnDisplayMemberChanged(e);
            RefreshDataSource();
        }

        protected override void OnValueMemberChanged(System.EventArgs e)
        {
            base.OnValueMemberChanged(e);
            RefreshDataSource();
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message m, System.Windows.Forms.Keys k)
        {
            return base.ProcessCmdKey(ref m, k);
        }

        public static void FlushCachedType(System.Type itemType)
        {
            if (Oranikle.Studio.Controls.CtrlNullableComboBox.StandardMap.ContainsKey(itemType))
                Oranikle.Studio.Controls.CtrlNullableComboBox.StandardMap.Remove(itemType);
            if (Oranikle.Studio.Controls.CtrlNullableComboBox.NullableMap.ContainsKey(itemType))
                Oranikle.Studio.Controls.CtrlNullableComboBox.NullableMap.Remove(itemType);
        }

    } // class CtrlNullableComboBox

}

