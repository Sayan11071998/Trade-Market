namespace TradeMarket.Utilities
{
    public static class GameString
    {
        #region Player
        public const string PlayerInputActionMove = "Move";
        public const string PlayerInputActionInventory = "Inventory";
        public const string PlayerAnimationFloatHorizontal = "Horizontal";
        public const string PlayerAnimationFloatVertical = "Vertical";
        public const string PlayerAnimationFloatLastHorizontal = "LastHorizontal";
        public const string PlayerAnimationFloatLastVertical = "LastVertical";
        public const string PlayerAnimationBoolIsWalking = "IsWalking";
        #endregion

        #region  NPC
        public const string Placeholder_ItemHaving = "{itemHaving}";
        public const string Placeholder_ItemDesired = "{itemDesired}";
        public const string NothingText = "nothing";
        public const string SomethingText = "something";
        #endregion

        #region UI
        public const string TradePrompt = "Trade with {0}?";
        public const string GiveItemText = "Give: {0}";
        public const string ReceiveItemText = "Receive: {0}";
        #endregion

        #region Tags
        public const string PlayerTag = "Player";
        #endregion
    }
}