﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Fishing.AstralCatches;
using CalamityMod.Items.Fishing.SunkenSeaCatches;
using CalamityMod.Projectiles.Pets;
using CalamityMod.Buffs.Pets;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class MolluskEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mollusk Enchantment");
            Tooltip.SetDefault(
@"'The world is your oyster'
Two shellfishes aid you in combat
When using any weapon you have a 10% chance to throw a returning seashell projectile
Summons a sea urchin to protect you
Effects of Giant Pearl and Amidias' Pendant
Effects of Aquatic Emblem and Enchanted Pearl
Effects of Ocean's Crest, Deep Diver, The Transformer, and Luxor's Gift
Summons a Baby Ghost Bell pet");
            DisplayName.AddTranslation(GameCulture.Chinese, "软壳魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'世界任你驰骋'
召唤2个海贝为你而战
拥有大珍珠和阿米迪亚斯之垂饰的效果");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 5;
            item.value = 150000;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(74, 97, 96);
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.ShellfishMinion))
            {
                //set bonus clams
                calamity.Call("SetSetBonus", player, "mollusk", true);
                player.maxMinions += 4;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.BuffType("Shellfish")) == -1)
                    {
                        player.AddBuff(calamity.BuffType("Shellfish"), 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.ProjectileType("Shellfish")] < 2)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("Shellfish"), (int)(1500.0 * (double)player.minionDamage), 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.GiantPearl))
            {
                calamity.GetItem("GiantPearl").UpdateAccessory(player, hideVisual);
            }

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.calamityToggles.AmidiasPendant))
            {
                calamity.GetItem("AmidiasPendant").UpdateAccessory(player, hideVisual);
            }

            calamity.GetItem("AquaticEmblem").UpdateAccessory(player, hideVisual);
            calamity.GetItem("EnchantedPearl").UpdateAccessory(player, hideVisual);
            mod.GetItem("VictideEnchant").UpdateAccessory(player, hideVisual);

            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();
            fargoPlayer.MolluskEnchant = true;
            fargoPlayer.AddPet(SoulConfig.Instance.calamityToggles.GhostBellPet, hideVisual, ModContent.BuffType<BabyGhostBellBuff>(), ModContent.ProjectileType<BabyGhostBell>());
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ModContent.ItemType<MolluskShellmet>());
            recipe.AddIngredient(ModContent.ItemType<MolluskShellplate>());
            recipe.AddIngredient(ModContent.ItemType<MolluskShelleggings>());
            recipe.AddIngredient(ModContent.ItemType<VictideEnchant>());
            recipe.AddIngredient(ModContent.ItemType<GiantPearl>());
            recipe.AddIngredient(ModContent.ItemType<AmidiasPendant>());
            recipe.AddIngredient(ModContent.ItemType<AquaticEmblem>());
            recipe.AddIngredient(ModContent.ItemType<EnchantedPearl>());
            recipe.AddIngredient(ModContent.ItemType<AbyssShocker>());
            recipe.AddIngredient(ModContent.ItemType<PolarisParrotfish>());
            recipe.AddIngredient(ModContent.ItemType<AmidiasTrident>());
            recipe.AddIngredient(ModContent.ItemType<EutrophicShank>());
            recipe.AddIngredient(ModContent.ItemType<Serpentine>());
            recipe.AddIngredient(ModContent.ItemType<RustedJingleBell>()); 

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
