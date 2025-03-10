﻿namespace Life.Application.Data;

/// <summary>
/// アイテム物質データ
/// </summary>
/// <param name="ItemMatterId">アイテム物質ID</param>
/// <param name="ItemId">アイテムID</param>
/// <param name="ItemName">アイテム名</param>
/// <param name="CanEquip">装備可能かどうか</param>
public record class ItemMatterData(string ItemMatterId, string ItemId, string ItemName, bool CanEquip) : IItemMatterData;
