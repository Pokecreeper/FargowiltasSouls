﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Fargowiltas.Items.Tiles;

namespace FargowiltasSouls.Items.Accessories.Forces.Calamity
{
    public class DevastationForce : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Devastation");
            Tooltip.SetDefault(
@"'Rain hell down on those who resist your power'
All armor bonuses from Wulfrum, Reaver, Plague Reaper, and Demonshade
Effects of Trinket of Chi, Fabled Tortoise Shell, and Plague Hive
Effects of Plagued Fuel Pack, The Bee, The Camper, and Profaned Soul Crystal
Summons several pets");
            DisplayName.AddTranslation(GameCulture.Chinese, "毁灭之力");
            Tooltip.AddTranslation(GameCulture.Chinese,
@"'让那些反抗你的人下地狱吧'
拥有软壳, 掠夺者和阿塔西亚的套装效果
拥有炫星, 龙蒿和魔影的套装效果
拥有大珍珠和阿米迪亚斯之垂饰的效果
拥有寓言龟壳和瘟疫蜂巢的效果
拥有星陨幻空石和星神游龙外壳的效果
拥有渎魂神物和蚀日尊戒的效果");

        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 11;
            item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            mod.GetItem("WulfrumEnchant").UpdateAccessory(player, hideVisual);
            mod.GetItem("ReaverEnchant").UpdateAccessory(player, hideVisual);
            mod.GetItem("PlagueReaperEnchant").UpdateAccessory(player, hideVisual);
            mod.GetItem("DemonShadeEnchant").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "WulfrumEnchant");
            recipe.AddIngredient(null, "ReaverEnchant");
            recipe.AddIngredient(null, "PlagueReaperEnchant");
            recipe.AddIngredient(null, "DemonShadeEnchant");

            recipe.AddTile(ModLoader.GetMod("Fargowiltas").TileType("CrucibleCosmosSheet"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
