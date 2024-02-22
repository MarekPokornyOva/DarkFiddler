#region using
using Fiddler;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
#endregion using

namespace DarkFiddler
{
	public class DarkFiddler : IFiddlerExtension
	{
		public void OnLoad()
		{
			TabControl.TabPageCollection tabPages = FiddlerApplication.UI.tabsViews.TabPages;
			List<TabPage> themedTabs = new List<TabPage>(10);
			foreach (TabPage tp in tabPages)
			{
				themedTabs.Add(tp);
				tp.ControlAdded += (sender, e) => DarkTheme.Apply((Control)sender);
			}

			DarkTheme.Apply(FiddlerApplication.UI);
			FiddlerApplication.UI.tabsViews.ControlAdded += (sender, e) =>
			{
				foreach (TabPage tp in tabPages)
					if (!themedTabs.Contains(tp))
					{
						DarkTheme.Apply(tp);
						themedTabs.Add(tp);
					}
			};
		}

		#region GetAllTypes - for dev
		//void GetAllTypes(object sender, EventArgs e)
		//{
		//	List<Type> types = new List<Type>();
		//	void AppendType(Control control)
		//	{
		//		Type t = control.GetType();
		//		if (!types.Contains(t))
		//			types.Add(t);
		//		foreach (Control c in control.Controls)
		//			AppendType(c);
		//	}
		//	AppendType(FiddlerApplication.UI);
		//	string s = string.Join("\r\n", types.Select(x => $"{x.Name}:{x.BaseType.Name}"));
		//}
		#endregion GetAllTypes - for dev

		public void OnBeforeUnload()
		{ }
	}

	static class DarkTheme
	{
		static readonly Color _foreColor = Color.White;
		static readonly Color _backColor = Color.FromArgb(30, 30, 30);
		static readonly Color _windowColor = Color.FromKnownColor(KnownColor.Window);
		static readonly Color _controlColor = Color.FromKnownColor(KnownColor.Control);
		static Control _txtBuilderRequestBody;

		internal static void Apply(Control control)
		{
			if (control.Name == "cbxBuilderURL")
				control.TextChanged += (sender, e) => { Control c = (Control)sender; if (c.BackColor == _windowColor) c.BackColor = _backColor; };
			if (control.Name == "txtBuilderRequestBody")
			{
				control.TextChanged += (sender, e) => { Control c = (Control)sender; if ((c.BackColor == _windowColor) || (c.BackColor == _controlColor)) c.BackColor = _backColor; };
				_txtBuilderRequestBody = control;
			}
			if (control.Name == "cbxBuilderMethod")
				control.TextChanged += (sender, e) => { Control c = _txtBuilderRequestBody; if ((c.BackColor == _windowColor) || (c.BackColor == _controlColor)) c.BackColor = _backColor; };

			string typeName = control.GetType().Name;
			if (typeName.Contains("RichTextBox5") /*|| typeName.Contains("HexBox")*/)
				return;

			control.ForeColor = _foreColor;
			control.BackColor = _backColor;

			foreach (Control c in control.Controls)
				Apply(c);
		}
	}
}
