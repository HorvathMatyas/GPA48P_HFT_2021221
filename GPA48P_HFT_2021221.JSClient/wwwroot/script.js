let actors = [];

fetch('http://localhost:62480/animalShelter')
    .then(x => x.json())
    .then(y => {
        shelters = y;
        console.log(shelters);
        display();
    });

function display() {
    shelters.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.shelterId + "</td><td>"
            + t.shelterName + "</td><td>"
            + t.address + "</td><td>"
            + t.phoneNumber + "</td><td>"
            + t.taxNumber + "</td></tr";
    });
}