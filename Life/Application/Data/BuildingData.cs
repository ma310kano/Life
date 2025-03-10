﻿namespace Life.Application.Data;

/// <summary>
/// 建造物データ
/// </summary>
/// <param name="BuildingId">建造物ID</param>
/// <param name="BuildingName">建造物名</param>
/// <param name="ItemRecipes">アイテムレシピのコレクション</param>
public record class BuildingData(string BuildingId, string BuildingName, IReadOnlyCollection<ItemRecipeSummaryData> ItemRecipes);
