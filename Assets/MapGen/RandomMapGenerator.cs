﻿using System;
using System.Collections.Generic;

using System.Text;

namespace MapGen
{
    public class RandomMapGenerator : MapGenerator
    {
        public RandomMapGenerator()
        {

        }

        public override Room[,] GenerateRooms(int maxRooms)
        {
            Directions[] directions = (Directions[])Enum.GetValues(typeof(Directions));
            List<Room> rooms = new List<Room>();
            rooms.Add(new Room(new Point(0, 0)));

            while (rooms.Count < maxRooms)
            {
                Room targetRoom = rooms[Randomizer.GetRandomNumber(rooms.Count)];
                Directions direction = Randomizer.GetRandomDirection();
                Exit targetExit = targetRoom.GetExit(direction);

                if (targetExit != null)
                    continue;

                targetExit = new Exit(direction);
                targetRoom.AddExit(direction);
                Room neighbor = GetRoomAtPoint(targetRoom.GetNeighborCoordinates(direction), rooms);
                Directions neighborDirection = ReverseDirection(direction);

                if (neighbor != null)
                    neighbor.AddExit(neighborDirection);
                else
                {
                    Room newRoom = new Room(targetRoom.GetNeighborCoordinates(direction));
                    newRoom.AddExit(neighborDirection);
                    rooms.Add(newRoom);
                }
            }
            Room[,] roomArray = ConvertListToMap(MarkSpecialRooms<Room>(rooms));

            return roomArray;
        }
    }
}
