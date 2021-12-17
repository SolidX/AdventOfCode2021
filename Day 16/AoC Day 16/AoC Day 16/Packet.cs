using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC_Day_16
{
    public class Packet
    {
        public int Version { get; set; }

        private PacketType _typeId;
        public PacketType TypeId
        {
            get { return _typeId; }
            set
            {
                if (value != PacketType.LiteralValue)
                    _value = null;
                _typeId = value;
            }
        }

        private ulong? _value = null;
        public ulong? Value
        { 
            get { return _value;  }
            set { if (TypeId == PacketType.LiteralValue) _value = value; }
        }

        public List<Packet> Subpackets { get; private set; }

        public Packet()
        {
            Subpackets = new List<Packet>();
        }

        public static Packet Create(Queue<bool> bits)
        {
            if (bits.Count > 6)
            {
                var version = bits.DequeueRange(3).ToByte();
                var typeId = bits.DequeueRange(3).ToByte();

                var p = new Packet()
                {
                    Version = version,
                    TypeId = (PacketType)typeId
                };

                if (typeId == 4)
                {
                    //Literal Value
                    var firstBit = true;
                    var val = new List<bool>();
                    do
                    {
                        var chunk = bits.DequeueRange(5);
                        firstBit = chunk.First();
                        val.AddRange(chunk.Skip(1));
                    }
                    while (bits.Count > 0 && firstBit);
                    p.Value = (ulong)val.ToBytes().ToUInt64();
                    return p;
                }
                else
                {
                    //Operator
                    var lengthTypeId = bits.Dequeue();
                    var subPackets = new List<Packet>();

                    if (!lengthTypeId)
                    {
                        //if 0, next 15 bits are the total length (in bits) of sub packets
                        var subPacketLength = bits.DequeueRange(15).ToBytes().ToUInt64();
                        var subPacketBits = new Queue<bool>(bits.DequeueRange((int)subPacketLength));

                        while (subPacketBits.Count > 0)
                            subPackets.Add(Packet.Create(subPacketBits));
                    }
                    else
                    {
                        //if 1, next 11 bits are a number of sub-packets contained in this packet
                        var subPacketCount = bits.DequeueRange(11).ToBytes().ToUInt64();

                        for (var i = 0u; i < subPacketCount; i++)
                            subPackets.Add(Packet.Create(bits));
                    }

                    p.Subpackets = subPackets;
                    return p;
                }
            }
            else
                return null; //Not enough bits to be a valid Packet
        }

        public ulong Evaluate()
        {
            switch ((PacketType)TypeId)
            {
                case PacketType.Sum:
                    Console.Write("+");
                    return Subpackets.Sum(sp => sp.Evaluate());
                case PacketType.Product:
                    Console.Write("*");
                    return Subpackets.Product(sp => sp.Evaluate());
                case PacketType.Minimum:
                    Console.Write("MIN(");
                    return Subpackets.Min(sp => sp.Evaluate());
                 case PacketType.Maximum:
                    Console.Write("MAX(");
                    return Subpackets.Max(sp => sp.Evaluate());                case PacketType.GreaterThan:
                    Console.Write(">");
                    return Subpackets.First().Evaluate() > Subpackets.Last().Evaluate() ? 1uL : 0uL;
                case PacketType.LessThan:
                    Console.Write("<");
                    return Subpackets.First().Evaluate() < Subpackets.Last().Evaluate() ? 1uL : 0uL;
                case PacketType.EqualTo:
                    Console.Write("==");
                    return Subpackets.First().Evaluate() == Subpackets.Last().Evaluate() ? 1uL : 0uL;
                case PacketType.LiteralValue:
                default:
                    Console.Write($" {Value.Value} ");
                    return Value.Value;
            }
        }
    }
}
