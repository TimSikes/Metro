
function submitStops() {
	var departure = $('#departureSelect').val();
	var arrival = $('#arrivalSelect').val();

	console.log("departure");
	console.log(departure);
	console.log("arrival");
	console.log(arrival);

	window.location.href = "/home/route?departure=" + departure + "&arrival=" + arrival;
}
