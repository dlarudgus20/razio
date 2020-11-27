using System;

const string map = @"########################################
#                                      #
#                                      #
#                                      #
#                                      #
#                                      #
#                                      #
#                                      #
#                                      #
O                                       O
#                                      #
#                                      #
#                                      #
#                                      #
#                                      #
#                                      #
#                                      #
#                                      #
########################################";

(int x, int y) position = (19, 9);

Console.Clear();

while (true)
{
    var keyInfo = Console.ReadKey();

    Console.SetCursorPosition(2, 2);
    Console.Write(map);
}
