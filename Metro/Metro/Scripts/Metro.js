
function submitStops() {
	var departure = $('#departureInput').val();
	var arrival = $('#arrivalInput').val();

	console.log("departure");
	console.log(departure);
	console.log("arrival");
	console.log(arrival);

	window.location.href = "/home/route?departure=" + departure + "&arrival=" + arrival;
}
