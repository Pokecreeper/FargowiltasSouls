﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Localization;
using CalamityMod.Items.Armor;
using CalamityMod.Items.Fishing.BrimstoneCragCatches;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Fishing.FishingRods;
using CalamityMod.Items.Pets;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class BrimflameEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brimflame Enchantment");
            Tooltip.SetDefault(
@"''
Press Y to trigger a brimflame frenzy effect
While under this effect, your damage is significantly boosted
However, this comes at the cost of rapid life loss and no mana regeneration
Summons a Brimling pet");
        }

        /*public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(191, 68, 59);
                }
            }
        }*/

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 7;
            item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            //set bonus
            calamity.Call("SetSetBonus", player, "brimflame", true);

            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();
            fargoPlayer.BrimflameEnchant = true;
            fargoPlayer.AddPet(SoulConfig.Instance.calamityToggles.BrimlingPet, hideVisual, calamity.BuffType("BrimlingBuff"), calamity.ProjectileType("BrimlingPet"));
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ModContent.ItemType<BrimflameScowl>());
            recipe.AddIngredient(ModContent.ItemType<BrimflameRobes>());
            recipe.AddIngredient(ModContent.ItemType<BrimflameBoots>());
            recipe.AddIngredient(ModContent.ItemType<DragoonDrizzlefish>());
            recipe.AddIngredient(ModContent.ItemType<Butcher>());
            recipe.AddIngredient(ModContent.ItemType<FulgurationHalberd>());
            recipe.AddIngredient(ModContent.ItemType<Roxcalibur>());
            recipe.AddIngredient(ModContent.ItemType<IgneousExaltation>());
            recipe.AddIngredient(ModContent.ItemType<BlazingStar>());
            recipe.AddIngredient(ModContent.ItemType<DormantBrimseeker>());
            recipe.AddIngredient(ModContent.ItemType<BrimstoneFlameblaster>());
            recipe.AddIngredient(ModContent.ItemType<TheEyeofCalamitas>());
            recipe.AddIngredient(ModContent.ItemType<SlurperPole>());
            recipe.AddIngredient(ModContent.ItemType<CharredRelic>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
