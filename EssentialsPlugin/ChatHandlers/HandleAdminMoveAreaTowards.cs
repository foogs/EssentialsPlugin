﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using EssentialsPlugin.Utility;

using Sandbox.ModAPI;
using Sandbox.Common.ObjectBuilders;

using VRageMath;

using SEModAPIInternal.API.Entity;
using SEModAPIInternal.API.Entity.Sector.SectorObject;

namespace EssentialsPlugin.ChatHandlers
{
	public class HandleAdminMoveAreaTowards : ChatHandlerBase
	{
		public override string GetHelp()
		{
			return "This command allows you to move ships and stations from one area towards another area.  Usage: /admin move area towards [SX] [SY] [SZ] [TX] [TY] [TZ] [DISTANCE] [RADIUS] where S is source and T is the target area you want to move towards.  Distance is the amount of meters you'd like to move towards the target point.";
		}
		public override string GetCommandText()
		{
			return "/admin move area towards";
		}

		public override bool IsAdminCommand()
		{
			return true;
		}

		public override bool AllowedInConsole()
		{
			return true;
		}

		// /admin movefrom x y z x y z radius
		public override bool HandleCommand(ulong userId, string[] words)
		{
			if (words.Count() != 8 && words.Count() != 0)
				return false;

			if (words.Count() != 8)
			{
				Communication.SendPrivateInformation(userId, GetHelp());
				return true;		
			}

			// Test Input
			float test = 0;
			for(int r = 0; r < 7; r++)
			{
				if(!float.TryParse(words[r], out test))
				{
					Communication.SendPrivateInformation(userId, string.Format("The value at position {0} - '{1}' is invalid.  Please try the command again.", r + 1, words[r]));
					return true;
				}
			}

			Vector3D startPosition = new Vector3(float.Parse(words[0]), float.Parse(words[1]), float.Parse(words[2]));
			Vector3D targetPosition = new Vector3(float.Parse(words[3]), float.Parse(words[4]), float.Parse(words[5]));
			float distance = float.Parse(words[6]);
			float radius = float.Parse(words[7]);

			Vector3D movementPosition = targetPosition - startPosition;
			movementPosition.Normalize();
			Vector3D finalPosition = movementPosition * distance;
			//finalPosition += startPosition;

			List<MyObjectBuilder_CubeGrid> gridsToMove = new List<MyObjectBuilder_CubeGrid>();
			BoundingSphere sphere = new BoundingSphere(startPosition, radius);
			List<IMyEntity> entitiesToMove = MyAPIGateway.Entities.GetEntitiesInSphere(ref sphere);

			Wrapper.GameAction(() =>
			{
				foreach (IMyEntity entity in entitiesToMove)
				{
					if (!(entity is IMyCubeGrid))
						continue;

					gridsToMove.Add((MyObjectBuilder_CubeGrid)entity.GetObjectBuilder());
					MyAPIGateway.Entities.RemoveEntity(entity);

					Logging.WriteLineAndConsole(string.Format("Moving '{0}' from {1} to {2}", entity.DisplayName, entity.GetPosition(), entity.GetPosition() + finalPosition));
				}
			});

			Thread.Sleep(5000);

			Wrapper.GameAction(() =>
			{
				foreach(IMyEntity entity in entitiesToMove)
				{
					if (!(entity is IMyCubeGrid))
						continue;

					MyAPIGateway.Entities.RemoveFromClosedEntities(entity);
					Logging.WriteLineAndConsole(string.Format("Removing '{0}' for move", entity.DisplayName));
				}
			});

			Thread.Sleep(10000);

			Wrapper.GameAction(() =>
			{
				foreach(MyObjectBuilder_CubeGrid grid in gridsToMove)
				{
					grid.PositionAndOrientation = new MyPositionAndOrientation(grid.PositionAndOrientation.Value.Position + finalPosition, grid.PositionAndOrientation.Value.Forward, grid.PositionAndOrientation.Value.Up);
					Logging.WriteLineAndConsole(string.Format("Adding '{0}' for move", grid.DisplayName));
					SectorObjectManager.Instance.AddEntity(new CubeGridEntity(grid));					
				}
			});

			return true;
		}
	}
}
