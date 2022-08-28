using BaseLibrary;
using BaseLibrary.UI;
using Microsoft.Xna.Framework;
using PortableStorageFluids.Items;
using Terraria;
using Terraria.Localization;

namespace PortableStorageFluids.UI;

public class BucketPanel : BaseTankPanel<Bucket>
{
	public BucketPanel(BaseTank tank) : base(tank)
	{
	}
}

public class DrumPanel : BaseTankPanel<Drum>
{
	public DrumPanel(BaseTank tank) : base(tank)
	{
	}
}

public class TankPanel : BaseTankPanel<Tank>
{
	public TankPanel(BaseTank tank) : base(tank)
	{
	}
}

public abstract class BaseTankPanel<T> : BaseUIPanel<T> where T : BaseTank
{
	public BaseTankPanel(BaseTank tank) : base((T)tank)
	{
		Width.Pixels = 300;
		Height.Pixels = 300;

		UIText textLabel = new UIText(Lang.GetItemNameValue(Container.Item.type))
		{
			X = { Percent = 50 },
			Settings = { HorizontalAlignment = HorizontalAlignment.Center }
		};
		Add(textLabel);

		UIText buttonClose = new UIText("X")
		{
			Height = { Pixels = 20 },
			Width = { Pixels = 20 },
			X = { Percent = 100 },
			HoverText = Language.GetText("Mods.PortableStorage.UI.Close")
		};
		buttonClose.OnMouseDown += args =>
		{
			if (args.Button != MouseButton.Left) return;

			PanelUI.Instance.CloseUI(Container);
			args.Handled = true;
		};
		buttonClose.OnMouseEnter += _ => buttonClose.Settings.TextColor = Color.Red;
		buttonClose.OnMouseLeave += _ => buttonClose.Settings.TextColor = Color.White;
		Add(buttonClose);

		UITank uitank = new UITank(tank.GetFluidStorage(), 0)
		{
			X = { Percent = 50 },
			Y = { Pixels = 28 },
			Height = { Percent = 100, Pixels = -28 },
			Width = { Pixels = 40 }
		};
		Add(uitank);
	}
}