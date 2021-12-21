using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace AoC_Day_18
{
    public class SnailfishNumber
    {
        private int? _leftNumber;
        private int? _rightNumber;
        private SnailfishNumber _leftNest;
        private SnailfishNumber _rightNest;
        public SnailfishNumber Parent { get; private set; }

        public dynamic Left
        {
            get { return IsLeftANumber ? _leftNumber : _leftNest; }
            set
            {
                var o = value;
                if (o.GetType() == typeof(Int32))
                {
                    _leftNumber = value;
                    _leftNest = null;
                    return;
                }
                if (o.GetType() == typeof(SnailfishNumber))
                {
                    _leftNest = value;
                    _leftNumber = null;
                    return;
                }
                throw new ArgumentException();
            }
        }

        public dynamic Right
        {
            get { return IsRightANumber ? _rightNumber : _rightNest; }
            set
            {
                var o = value;
                if (o.GetType() == typeof(Int32))
                {
                    _rightNumber = value;
                    _rightNest = null;
                    return;
                }
                if (o.GetType() == typeof(SnailfishNumber))
                {
                    _rightNest = value;
                    _rightNumber = null;
                    return;
                }
                throw new ArgumentException();
            }
        }

        public bool IsLeftANumber => _leftNumber.HasValue && _leftNest == null;
        public bool IsRightANumber => _rightNumber.HasValue && _rightNest == null;
        public bool IsLeftASnailfishNumber => !_leftNumber.HasValue && _leftNest != null;
        public bool IsRightASnailfishNumber => !_rightNumber.HasValue && _rightNest != null;

        public SnailfishNumber()
        {
            _leftNumber = null;
            _rightNumber = null;
            _leftNest = null;
            _rightNest = null;
            Parent = null;
        }

        public SnailfishNumber(JsonElement e, SnailfishNumber parent = null)
        {
            if (e[0].ValueKind == JsonValueKind.Number)
                _leftNumber = e[0].GetInt32();
            else
                _leftNest = new SnailfishNumber(e[0], this);

            if (e[1].ValueKind == JsonValueKind.Number)
                _rightNumber = e[1].GetInt32();
            else
                _rightNest = new SnailfishNumber(e[1], this);

            Parent = parent;
        }

        private SnailfishNumber(SnailfishNumber n, SnailfishNumber parent = null)
        {
            var copy = new SnailfishNumber();

            if (n.IsLeftANumber)
                copy.Left = n.Left;
            else
                copy.Left = new SnailfishNumber(copy.Left, this);

            if (n.IsRightANumber)
                copy.Right = n.Right;
            else
                copy.Right = new SnailfishNumber(copy.Right, this);

            Parent = parent;
        }

        public int Magnitude()
        {
            return Magnitude(this);
        }

        /// <summary>
        /// The magnitude of a pair is 3 times the magnitude of its left element plus 2 times the magnitude of its right element.
        /// The magnitude of a regular number is just that number.
        /// </summary>
        /// <param name="n">A "snailfish number"</param>
        /// <exception cref="ArgumentException">If <paramref name="n"/> is not an Int32 or a SnailfishNumber.</exception>
        /// <returns>The magnitude of a given SnailfishNubmer or integer</returns>
        public static int Magnitude(dynamic n)
        {
            if (n.GetType() == typeof(SnailfishNumber))
            {
                var leftMost = Magnitude(n.Left);
                var rightMost = Magnitude(n.Right);

                return (3 * leftMost) + (2 * rightMost);
            }
            if (n.GetType() == typeof(int))
            {
                return (int)n;
            }
            throw new ArgumentException();
        }

        /// <summary>
        /// Gets the "split" of a regular number.
        /// To split a regular number, replace it with a pair; the left element of the pair should be the regular number divided by two and rounded down,
        /// while the right element of the pair should be the regular number divided by two and rounded up.
        /// </summary>
        /// <param name="n">A number to "split"</param>
        /// <returns>A SnailfishNumber representing the he "split" of an integer</returns>
        public static SnailfishNumber Split(int n, SnailfishNumber parent = null)
        {
            var half = (decimal)n / 2m;
            return new SnailfishNumber() { Left = (int)Math.Floor(half), Right = (int)Math.Ceiling(half), Parent = parent };
        }

        public SnailfishNumber DeepCopy()
        {
            return new SnailfishNumber(this);
        }

        public static SnailfishNumber DeepCopy(SnailfishNumber n)
        {
            return new SnailfishNumber(n);
        }

        public override string ToString()
        {
            var str = $"[{(IsLeftANumber ? _leftNumber : _leftNest.ToString())},";
            str += $"{(IsRightANumber ? _rightNumber : _rightNest.ToString())}]";

            return str;
        }

        /// <summary>
        /// To add two snailfish numbers, form a pair from the left and right parameters of the addition operator.
        /// </summary>
        /// <param name="a">Left-hand side operand</param>
        /// <param name="b">Right-hand side operand</param>
        /// <returns>A SnailfishNumber</returns>
        /// <example>[1,2] + [[3,4],5] becomes [[1,2],[[3,4],5]]</example>
        public static SnailfishNumber operator +(SnailfishNumber a, SnailfishNumber b)
        {
            var n = new SnailfishNumber() { Left = a, Right = b };
            a.Parent = n;
            b.Parent = n;

            n.FullyReduce();

            return n;
        }

        /// <summary>
        /// To explode a pair, the pair's left value is added to the first regular number to the left of the exploding pair (if any),
        /// and the pair's right value is added to the first regular number to the right of the exploding pair (if any).
        /// Exploding pairs will always consist of two regular numbers.
        /// Then, the entire exploding pair is replaced with the regular number 0.
        /// </summary>
        /// <param name="sn">A SnailfishNumber</param>
        /// <remarks>This problem is so frustratingly asinine that this is the fastest way to come to a working solution that doesn't result in another 12 hours of debugging "SnailfishNumber" traversal</remarks>
        public static SnailfishNumber Explodez(SnailfishNumber sn)
        {
            var numbers = new HashSet<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var asStr = sn.ToString();
            var depth = 0;

            var leftVal = "";
            var rightVal = "";
            var comma = false;

            int? beginReplace = null;

            for (int i = 0; i < asStr.Length; i++)
            {
                char c = asStr[i];
                if (c == '[')
                {
                    depth++;
                    comma = false;
                    leftVal = "";
                    rightVal = "";
                    continue;
                }
                if (c == ']')
                {
                    depth--;

                    if (depth >= 4)
                    {
                        beginReplace = i;
                        break;
                    }
                    else
                        continue;
                }

                if (depth >= 4)
                {
                    if (numbers.Contains(c))
                    {
                        if (!comma)
                            leftVal += c;
                        else
                            rightVal += c;   
                    }
                    else
                    {
                        if (!comma && c == ',')
                        {
                            comma = true;
                            continue;
                        }
                        else
                        {
                            beginReplace = asStr.IndexOf(']', i);
                            break;
                        }
                    }
                }
            }

            if (beginReplace.HasValue)
            {
                //Find left
                var startSn = asStr.Substring(0, asStr.LastIndexOf('[', beginReplace.Value));
                var leftOperand = "";
                int? lefttOperandIdx = null;
                for (int i = startSn.Length - 1; i >= 0; i--)
                {
                    char c = startSn[i];
                    if (numbers.Contains(c))
                    {
                        leftOperand += c;
                        lefttOperandIdx = i;
                    }
                    else
                    {
                        if (leftOperand.Length > 0)
                            break;
                    }
                }
                leftOperand = new String(leftOperand.Reverse().ToArray());


                //Find right
                var endSn = asStr.Substring(beginReplace.Value + 1);
                var rightOperand = "";
                int? rightOperandIdx = null;
                for (int i = 0; i < endSn.Length; i++)
                {
                    char c = endSn[i];
                    if (numbers.Contains(c))
                    {
                        rightOperand += c;
                        rightOperandIdx = i;
                    }
                    else
                    {
                        if (rightOperand.Length > 0)
                            break;
                    }
                }

                //combine            
                var newSn = new StringBuilder();
                if (lefttOperandIdx.HasValue)
                {
                    var leftSum = Int32.Parse(leftVal) + Int32.Parse(leftOperand);
                    newSn.Append(startSn.Substring(0, lefttOperandIdx.Value));
                    newSn.Append(leftSum);
                    newSn.Append(startSn.Substring(lefttOperandIdx.Value + leftOperand.Length));
                }
                else
                    newSn.Append(startSn);

                newSn.Append(0);

                if (rightOperandIdx.HasValue)
                {
                    var rightSum = Int32.Parse(rightVal) + Int32.Parse(rightOperand);
                    newSn.Append(endSn.Substring(0, rightOperandIdx.Value + 1).Trim(numbers.ToArray()));
                    newSn.Append(rightSum);
                    newSn.Append(endSn.Substring(rightOperandIdx.Value + 1));
                }
                else
                    newSn.Append(endSn);

                //parse
                return InputParser.Parse(newSn.ToString());
            }
            else
                return sn; //Nothing to explode
        }
        public void FullyReduce()
        {
            string prev;
            var hasChanged = true;
            
            while (hasChanged)
            {
                prev = ToString();

                var exploded = Explodez(this);
                hasChanged = !prev.Equals(exploded.ToString(), StringComparison.CurrentCultureIgnoreCase);

                Left = exploded.Left;
                Right = exploded.Right;
                Parent = exploded.Parent;

                if (!hasChanged)
                    hasChanged = SplitFirstAvailable(this);
            }
        }

        private static bool SplitFirstAvailable(SnailfishNumber root)
        {
            if (root.IsLeftASnailfishNumber)
            {
                var leftSplit = SplitFirstAvailable(root.Left);
                if (leftSplit)
                    return true;
            }
            else
            {
                if (root._leftNumber >= 10)
                {
                    root.Left = Split(root._leftNumber.Value, root);
                    return true;
                }
            }

            if (root.IsRightASnailfishNumber)
                return SplitFirstAvailable(root.Right);
            else
            {
                if (root._rightNumber >= 10)
                {
                    root.Right = Split(root._rightNumber.Value, root);
                    return true;
                }
            }

            return false;
        }

        private int GetDepth()
        {
            var depth = 0;

            var p = Parent;
            while (p != null)
            {
                depth++;
                p = p.Parent;
            }

            return depth;
        }
    }
}
