using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.Config;
using System.ComponentModel;

namespace iFrameCounter
{
	public class IFrameCounter : Mod
	{
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int mouseText = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
			if (mouseText != -1)
			{
				layers.Insert(mouseText, new LegacyGameInterfaceLayer("iFrameCounter: iFrame Counter", delegate
				{
					if (!Main.gameMenu && Main.LocalPlayer.immuneTime != 0 && ModContent.GetInstance<Config>().iFrameCounter && !Main.LocalPlayer.dead)
					{
						string text = string.Format("{0:f" + ModContent.GetInstance<Config>().Decimal + "}", Main.LocalPlayer.immuneTime / 60f);

						Vector2 position = ModContent.GetInstance<Config>().Position * new Vector2(Main.screenWidth, Main.screenHeight);

						Utils.DrawBorderString(Main.spriteBatch, text, position, Color.WhiteSmoke);
					}
					return true;
				}, InterfaceScaleType.UI));
			}
		}
	}

	public class Config : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		[Header("iFrames")]

		[Label("iFrame Counter")]
		[Tooltip("Adds a timer next to the player to show how much iFrames are left. False for no timer, true to for timer. True by default")]
		[DefaultValue(true)]
		public bool iFrameCounter;

		[Label("iFrame Counter Decimal")]
		[Tooltip("The amount of decimals to show on the iFrame counter. Default 1")]
		[DrawTicks]
		[Range(0, 2)]
		[Increment(1)]
		[DefaultValue(1)]
		public int Decimal;

		[Label("iFrame Counter Position")]
		[Tooltip("Changes the position of the iFrame counter")]
		[DefaultValue(typeof(Vector2), "0.47, 0.5")]
		public Vector2 Position;
	}
}