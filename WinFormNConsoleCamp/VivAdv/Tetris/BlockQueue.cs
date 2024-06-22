

namespace Tetris
{
    public class BlockQueue
    {
        private readonly Block[] blocks =
        [
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock()
        ];

        private readonly Random random = new();

        public Block NextBlock { get; set; }

        private Block RandomBlock()
        {
            return blocks[random.Next(blocks.Length)];
        }

        public BlockQueue()
        {
            NextBlock = RandomBlock();
        }

        public Block GetAndUpdate()
        {
            Block block = NextBlock;

            do
            {
                NextBlock = RandomBlock();
            } while (block.Id == NextBlock.Id);

            return block;
        }
    }
}
