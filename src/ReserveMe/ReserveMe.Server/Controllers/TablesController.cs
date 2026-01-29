namespace ReserveMe.Server.Controllers
{
	using Application.Tables.Queries;
	using Microsoft.AspNetCore.Mvc;
	using Shared.Dtos.Tables;

	public class TablesController : ApiControllerBase
	{
		#region READ

		[HttpGet("getTablesByVenueId/{venueId}")]
		public async Task<ActionResult<List<TableDto>>> GetTables(int venueId)
		{
			return await Mediator.Send(new GetTablesQuery(venueId));
		}

		[HttpGet("getAvailableTables")]
		public async Task<ActionResult<List<TableDto>>> GetAvailableTables(int venueId, DateTime reservationTime, int guestsCount)
		{
			return await Mediator.Send(new GetAvailableTablesQuery(venueId, reservationTime, guestsCount));
		}

		#endregion

		#region CREATE

		[HttpPost("create")]
		public async Task<ActionResult<int>> CreateTable(TableDto table)
		{
			return await Mediator.Send(new Application.Tables.Commands.CreateTableCommand(table));
		}

		#endregion

		#region UPDATE

		[HttpPut("update")]
		public async Task<ActionResult> UpdateTable(TableDto table)
		{
			await Mediator.Send(new Application.Tables.Commands.UpdateTableCommand(table));
			return NoContent();
		}

		#endregion

		#region DELETE

		[HttpDelete("delete/{tableId}")]
		public async Task<ActionResult> DeleteTable(int tableId)
		{
			await Mediator.Send(new Application.Tables.Commands.DeleteTableCommand(tableId));
			return NoContent();
		}

		#endregion
	}
}
