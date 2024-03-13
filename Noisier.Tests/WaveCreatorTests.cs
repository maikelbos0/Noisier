using NSubstitute;
using System.Text;
using Xunit;

namespace Noisier.Tests;

public class WaveCreatorTests {
    [Theory]
    [InlineData(30, 88200)]
    [InlineData(60, 44100)]
    [InlineData(120, 22050)]
    public void NoteDuration(uint beatsPerMinute, double expectedDuration) {
        var subject = new WaveCreator() {
            BeatsPerMinute = beatsPerMinute
        };

        Assert.Equal(subject.BeatDuration, expectedDuration);
    }

    [Theory]
    [InlineData(240, 1, 1, 0, 1, 44100)]
    [InlineData(240, 1, 5, 0, 1, 8820)]
    [InlineData(240, 2, 1, 0, 1, 88200)]
    [InlineData(240, 1, 1, 7, 1, 352800)]
    public void ChunkSize(uint beatsPerMinute, uint durationNumerator, uint durationDenominator, uint positionNumerator, uint positionDenominator, uint expectedChunkSize) {
        var subject = new WaveCreator() {
            BeatsPerMinute = beatsPerMinute,
            Tracks = {
                new() {
                    Notes = {
                        new() {
                            Pitches = { new(PitchClass.C, 4) },
                            Duration = new Fraction(durationNumerator, durationDenominator),
                            Position = new Fraction(positionNumerator, positionDenominator)
                        }
                    }
                }
            }
        };

        Assert.Equal(expectedChunkSize, subject.ChunkSize);
    }

    [Fact]
    public void WriteHeader() {
        var binaryWriter = Substitute.For<BinaryWriter>();
        var subject = new WaveCreator();

        subject.WriteHeader(binaryWriter);

        Received.InOrder(() => {
            binaryWriter.Write(Arg.Is<byte[]>(value => value.SequenceEqual(Encoding.ASCII.GetBytes("RIFF"))));
            binaryWriter.Write((uint)44);
            binaryWriter.Write(Arg.Is<byte[]>(value => value.SequenceEqual(Encoding.ASCII.GetBytes("WAVE"))));
        });
    }

    [Fact]
    public void WriteFormat() {
        var binaryWriter = Substitute.For<BinaryWriter>();
        var subject = new WaveCreator();

        subject.WriteFormat(binaryWriter);

        Received.InOrder(() => {
            binaryWriter.Write(Arg.Is<byte[]>(value => value.SequenceEqual(Encoding.ASCII.GetBytes("fmt "))));
            binaryWriter.Write((uint)16);
            binaryWriter.Write((ushort)1);
            binaryWriter.Write((ushort)2);
            binaryWriter.Write((uint)44100);
            binaryWriter.Write((uint)176400);
            binaryWriter.Write((ushort)4);
            binaryWriter.Write((ushort)16);
        });
    }

    [Fact]
    public void WriteContent() {
        var binaryWriter = Substitute.For<BinaryWriter>();
        var subject = new WaveCreator() {
            BeatsPerMinute = 441,
            Tracks = {
                new() {
                    Notes = {
                        new() {
                            Pitches = { new(PitchClass.A, 3) },
                            Duration = new Fraction(1, 40),
                            Position = new Fraction(0, 40)
                        },
                        new() {
                            Pitches = { new(PitchClass.A, 4) },
                            Duration = new Fraction(1, 40),
                            Position = new Fraction(2, 40)
                        },
                        new() {
                            Pitches = { new(PitchClass.C, 4) },
                            Duration = new Fraction(1, 40),
                            Position = new Fraction(0, 40)
                        },
                        new() {
                            Pitches = { new(PitchClass.E, 4) },
                            Duration = new Fraction(1, 40),
                            Position = new Fraction(0, 40)
                        }
                    }
                }
            }
        };

        subject.WriteContent(binaryWriter);

        Received.InOrder(() => {
            binaryWriter.Write(Arg.Is<byte[]>(value => value.SequenceEqual(Encoding.ASCII.GetBytes("data"))));
            binaryWriter.Write((uint)1800);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)1155);
            binaryWriter.Write((short)2309);
            binaryWriter.Write((short)3459);
            binaryWriter.Write((short)4603);
            binaryWriter.Write((short)5740);
            binaryWriter.Write((short)6868);
            binaryWriter.Write((short)7984);
            binaryWriter.Write((short)9088);
            binaryWriter.Write((short)10177);
            binaryWriter.Write((short)11250);
            binaryWriter.Write((short)12305);
            binaryWriter.Write((short)13340);
            binaryWriter.Write((short)14354);
            binaryWriter.Write((short)15345);
            binaryWriter.Write((short)16311);
            binaryWriter.Write((short)17251);
            binaryWriter.Write((short)18163);
            binaryWriter.Write((short)19046);
            binaryWriter.Write((short)19899);
            binaryWriter.Write((short)20721);
            binaryWriter.Write((short)21509);
            binaryWriter.Write((short)22263);
            binaryWriter.Write((short)22982);
            binaryWriter.Write((short)23664);
            binaryWriter.Write((short)24309);
            binaryWriter.Write((short)24915);
            binaryWriter.Write((short)25482);
            binaryWriter.Write((short)26009);
            binaryWriter.Write((short)26495);
            binaryWriter.Write((short)26940);
            binaryWriter.Write((short)27342);
            binaryWriter.Write((short)27702);
            binaryWriter.Write((short)28018);
            binaryWriter.Write((short)28291);
            binaryWriter.Write((short)28520);
            binaryWriter.Write((short)28704);
            binaryWriter.Write((short)28845);
            binaryWriter.Write((short)28941);
            binaryWriter.Write((short)28992);
            binaryWriter.Write((short)29000);
            binaryWriter.Write((short)28963);
            binaryWriter.Write((short)28882);
            binaryWriter.Write((short)28757);
            binaryWriter.Write((short)28589);
            binaryWriter.Write((short)28378);
            binaryWriter.Write((short)28125);
            binaryWriter.Write((short)27830);
            binaryWriter.Write((short)27494);
            binaryWriter.Write((short)27117);
            binaryWriter.Write((short)26701);
            binaryWriter.Write((short)26245);
            binaryWriter.Write((short)25752);
            binaryWriter.Write((short)25222);
            binaryWriter.Write((short)24656);
            binaryWriter.Write((short)24055);
            binaryWriter.Write((short)23421);
            binaryWriter.Write((short)22753);
            binaryWriter.Write((short)22055);
            binaryWriter.Write((short)21326);
            binaryWriter.Write((short)20569);
            binaryWriter.Write((short)19785);
            binaryWriter.Write((short)18974);
            binaryWriter.Write((short)18140);
            binaryWriter.Write((short)17282);
            binaryWriter.Write((short)16403);
            binaryWriter.Write((short)15504);
            binaryWriter.Write((short)14586);
            binaryWriter.Write((short)13652);
            binaryWriter.Write((short)12703);
            binaryWriter.Write((short)11741);
            binaryWriter.Write((short)10766);
            binaryWriter.Write((short)9781);
            binaryWriter.Write((short)8788);
            binaryWriter.Write((short)7788);
            binaryWriter.Write((short)6783);
            binaryWriter.Write((short)5775);
            binaryWriter.Write((short)4764);
            binaryWriter.Write((short)3754);
            binaryWriter.Write((short)2744);
            binaryWriter.Write((short)1738);
            binaryWriter.Write((short)737);
            binaryWriter.Write((short)-257);
            binaryWriter.Write((short)-1244);
            binaryWriter.Write((short)-2222);
            binaryWriter.Write((short)-3189);
            binaryWriter.Write((short)-4143);
            binaryWriter.Write((short)-5084);
            binaryWriter.Write((short)-6009);
            binaryWriter.Write((short)-6918);
            binaryWriter.Write((short)-7808);
            binaryWriter.Write((short)-8679);
            binaryWriter.Write((short)-9530);
            binaryWriter.Write((short)-10358);
            binaryWriter.Write((short)-11164);
            binaryWriter.Write((short)-11946);
            binaryWriter.Write((short)-12702);
            binaryWriter.Write((short)-13432);
            binaryWriter.Write((short)-14135);
            binaryWriter.Write((short)-14811);
            binaryWriter.Write((short)-15457);
            binaryWriter.Write((short)-16073);
            binaryWriter.Write((short)-16660);
            binaryWriter.Write((short)-17215);
            binaryWriter.Write((short)-17738);
            binaryWriter.Write((short)-18229);
            binaryWriter.Write((short)-18688);
            binaryWriter.Write((short)-19113);
            binaryWriter.Write((short)-19505);
            binaryWriter.Write((short)-19864);
            binaryWriter.Write((short)-20188);
            binaryWriter.Write((short)-20478);
            binaryWriter.Write((short)-20734);
            binaryWriter.Write((short)-20956);
            binaryWriter.Write((short)-21143);
            binaryWriter.Write((short)-21296);
            binaryWriter.Write((short)-21415);
            binaryWriter.Write((short)-21500);
            binaryWriter.Write((short)-21551);
            binaryWriter.Write((short)-21570);
            binaryWriter.Write((short)-21555);
            binaryWriter.Write((short)-21508);
            binaryWriter.Write((short)-21428);
            binaryWriter.Write((short)-21318);
            binaryWriter.Write((short)-21176);
            binaryWriter.Write((short)-21004);
            binaryWriter.Write((short)-20802);
            binaryWriter.Write((short)-20572);
            binaryWriter.Write((short)-20314);
            binaryWriter.Write((short)-20028);
            binaryWriter.Write((short)-19715);
            binaryWriter.Write((short)-19378);
            binaryWriter.Write((short)-19015);
            binaryWriter.Write((short)-18629);
            binaryWriter.Write((short)-18220);
            binaryWriter.Write((short)-17789);
            binaryWriter.Write((short)-17338);
            binaryWriter.Write((short)-16867);
            binaryWriter.Write((short)-16378);
            binaryWriter.Write((short)-15871);
            binaryWriter.Write((short)-15348);
            binaryWriter.Write((short)-14810);
            binaryWriter.Write((short)-14258);
            binaryWriter.Write((short)-13694);
            binaryWriter.Write((short)-13117);
            binaryWriter.Write((short)-12530);
            binaryWriter.Write((short)-11934);
            binaryWriter.Write((short)-11330);
            binaryWriter.Write((short)-10718);
            binaryWriter.Write((short)-10101);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)-427);
            binaryWriter.Write((short)199);
            binaryWriter.Write((short)825);
            binaryWriter.Write((short)1448);
            binaryWriter.Write((short)2065);
            binaryWriter.Write((short)2674);
            binaryWriter.Write((short)3272);
            binaryWriter.Write((short)3858);
            binaryWriter.Write((short)4428);
            binaryWriter.Write((short)4981);
            binaryWriter.Write((short)5514);
            binaryWriter.Write((short)6026);
            binaryWriter.Write((short)6514);
            binaryWriter.Write((short)6977);
            binaryWriter.Write((short)7412);
            binaryWriter.Write((short)7818);
            binaryWriter.Write((short)8193);
            binaryWriter.Write((short)8536);
            binaryWriter.Write((short)8846);
            binaryWriter.Write((short)9120);
            binaryWriter.Write((short)9359);
            binaryWriter.Write((short)9562);
            binaryWriter.Write((short)9726);
            binaryWriter.Write((short)9852);
            binaryWriter.Write((short)9940);
            binaryWriter.Write((short)9989);
            binaryWriter.Write((short)9998);
            binaryWriter.Write((short)9968);
            binaryWriter.Write((short)9899);
            binaryWriter.Write((short)9791);
            binaryWriter.Write((short)9645);
            binaryWriter.Write((short)9460);
            binaryWriter.Write((short)9239);
            binaryWriter.Write((short)8981);
            binaryWriter.Write((short)8688);
            binaryWriter.Write((short)8361);
            binaryWriter.Write((short)8001);
            binaryWriter.Write((short)7609);
            binaryWriter.Write((short)7188);
            binaryWriter.Write((short)6738);
            binaryWriter.Write((short)6262);
            binaryWriter.Write((short)5761);
            binaryWriter.Write((short)5238);
            binaryWriter.Write((short)4694);
            binaryWriter.Write((short)4132);
            binaryWriter.Write((short)3553);
            binaryWriter.Write((short)2961);
            binaryWriter.Write((short)2356);
            binaryWriter.Write((short)1743);
            binaryWriter.Write((short)1123);
            binaryWriter.Write((short)498);
            binaryWriter.Write((short)-128);
            binaryWriter.Write((short)-754);
            binaryWriter.Write((short)-1377);
            binaryWriter.Write((short)-1995);
            binaryWriter.Write((short)-2605);
            binaryWriter.Write((short)-3205);
            binaryWriter.Write((short)-3792);
            binaryWriter.Write((short)-4364);
            binaryWriter.Write((short)-4919);
            binaryWriter.Write((short)-5455);
            binaryWriter.Write((short)-5969);
            binaryWriter.Write((short)-6460);
            binaryWriter.Write((short)-6926);
            binaryWriter.Write((short)-7364);
            binaryWriter.Write((short)-7773);
            binaryWriter.Write((short)-8152);
            binaryWriter.Write((short)-8499);
            binaryWriter.Write((short)-8812);
            binaryWriter.Write((short)-9091);
            binaryWriter.Write((short)-9334);
            binaryWriter.Write((short)-9540);
            binaryWriter.Write((short)-9709);
            binaryWriter.Write((short)-9840);
            binaryWriter.Write((short)-9932);
            binaryWriter.Write((short)-9985);
            binaryWriter.Write((short)-9999);
            binaryWriter.Write((short)-9974);
            binaryWriter.Write((short)-9909);
            binaryWriter.Write((short)-9805);
            binaryWriter.Write((short)-9663);
            binaryWriter.Write((short)-9483);
            binaryWriter.Write((short)-9266);
            binaryWriter.Write((short)-9012);
            binaryWriter.Write((short)-8723);
            binaryWriter.Write((short)-8400);
            binaryWriter.Write((short)-8043);
            binaryWriter.Write((short)-7655);
            binaryWriter.Write((short)-7237);
            binaryWriter.Write((short)-6791);
            binaryWriter.Write((short)-6318);
            binaryWriter.Write((short)-5820);
            binaryWriter.Write((short)-5299);
            binaryWriter.Write((short)-4757);
            binaryWriter.Write((short)-4197);
            binaryWriter.Write((short)-3620);
            binaryWriter.Write((short)-3029);
            binaryWriter.Write((short)-2426);
            binaryWriter.Write((short)-1813);
            binaryWriter.Write((short)-1193);
            binaryWriter.Write((short)-569);
            binaryWriter.Write((short)56);
            binaryWriter.Write((short)683);
            binaryWriter.Write((short)1307);
            binaryWriter.Write((short)1925);
            binaryWriter.Write((short)2536);
            binaryWriter.Write((short)3137);
            binaryWriter.Write((short)3726);
            binaryWriter.Write((short)4300);
            binaryWriter.Write((short)4857);
            binaryWriter.Write((short)5395);
            binaryWriter.Write((short)5912);
            binaryWriter.Write((short)6406);
            binaryWriter.Write((short)6874);
            binaryWriter.Write((short)7315);
            binaryWriter.Write((short)7728);
            binaryWriter.Write((short)8111);
            binaryWriter.Write((short)8461);
            binaryWriter.Write((short)8778);
            binaryWriter.Write((short)9061);
            binaryWriter.Write((short)9308);
            binaryWriter.Write((short)9519);
            binaryWriter.Write((short)9692);
            binaryWriter.Write((short)9827);
            binaryWriter.Write((short)9924);
            binaryWriter.Write((short)9981);
            binaryWriter.Write((short)9999);
            binaryWriter.Write((short)9978);
            binaryWriter.Write((short)9918);
            binaryWriter.Write((short)9819);
            binaryWriter.Write((short)9681);
            binaryWriter.Write((short)9506);
            binaryWriter.Write((short)9293);
            binaryWriter.Write((short)9043);
            binaryWriter.Write((short)8758);
            binaryWriter.Write((short)8438);
            binaryWriter.Write((short)8085);
            binaryWriter.Write((short)7701);
            binaryWriter.Write((short)7286);
            binaryWriter.Write((short)6843);
            binaryWriter.Write((short)6373);
            binaryWriter.Write((short)5877);
            binaryWriter.Write((short)5359);
            binaryWriter.Write((short)4820);
            binaryWriter.Write((short)4261);
            binaryWriter.Write((short)3686);
            binaryWriter.Write((short)3096);
            binaryWriter.Write((short)2495);
            binaryWriter.Write((short)1883);
            binaryWriter.Write((short)1264);
        });
    }

    [Fact]
    public void GetSize() {
        var subject = new WaveCreator() {
            BeatsPerMinute = 60,
            Tracks = {
                new() {
                    Notes = {
                        new() {
                            Pitches = { new(PitchClass.C, 4) },
                            Duration = new Fraction(1, 1),
                            Position = new Fraction(23, 1)
                        }
                    }
                }
            }
        };

        Assert.Equal((uint)4233644, subject.GetSize());
    }
}
