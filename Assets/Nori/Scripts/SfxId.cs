namespace Nori
{
    /// <summary>對應 Assets/Audio 內音效檔（依檔名語意）。</summary>
    public enum SfxId
    {
        /// <summary>按鈕音效？.ogg</summary>
        Button = 0,
        /// <summary>放置音效.ogg</summary>
        Place = 1,
        /// <summary>遊戲失敗音效.ogg</summary>
        GameFail = 2,
        /// <summary>碰撞音效.ogg</summary>
        Collision = 3,
        /// <summary>噴大便音效.ogg</summary>
        PoopShoot = 4,
        /// <summary>攻擊前提示音效.ogg</summary>
        AttackWarning = 5,
        /// <summary>遊戲開始音效.ogg</summary>
        GameStart = 6,
        /// <summary>文具 Hover（可先與 Button 同 clip，錄好後在 AudioLibrary 換檔）</summary>
        StationeryHover = 7,
        /// <summary>文具滾輪旋轉 tick（可先與 Button 同 clip）</summary>
        StationeryRotate = 8,
        /// <summary>遊戲勝利音效.ogg</summary>
        GameWin = 9,
        Rubychan = 10,
        RubychanHai = 11,
        LookingMyEyes = 12,
        Nenene = 13,
    }
}
