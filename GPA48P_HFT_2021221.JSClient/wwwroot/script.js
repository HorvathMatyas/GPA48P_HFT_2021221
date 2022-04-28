let shelters = [];
let connection = null;
let shelterIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:62480/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("AnimalShelterCreated", (user, message) => {
        getdata();
    });

    connection.on("AnimalShelterDeleted", (user, message) => {
        getdata();
    });

    connection.on("AnimalShelterUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });

    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:62480/animalShelter')
        .then(x => x.json())
        .then(y => {
            shelters = y;
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    shelters.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>"
            + t.shelterId + "</td><td>"
            + t.shelterName + "</td><td>"
            + t.address + "</td><td>"
            + t.phoneNumber + "</td><td>"
            + t.taxNumber + "</td><td>"
            + `<button type="button" onclick="showupdate(${t.shelterId})">Update</button>` +
            + `<button type="button" onclick="remove(${t.shelterId})">Delete</button>` +
            "</td></tr>";
    });
}

function create() {
    let name = document.getElementById('shelterName').value;
    let name2 = document.getElementById('address').value;
    let name3 = document.getElementById('phoneNumber').value;
    let name4 = document.getElementById('taxNumber').value;
    fetch('http://localhost:62480/animalShelter', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                shelterName: name,
                address: name2,
                phoneNumber: name3,
                taxNumber: name4
            })})
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error); });
}

function showupdate(id) {
    document.getElementById('shelterNameToUpdate').value = shelter.find(t => t['shelterId'] == id)['shelterName'];
    document.getElementById('updateformdiv').style.display = 'flex';
    shelterIdToUpdate = id;
}

function update() {
    let name = document.getElementById('shelterNameToUpdate').value;
    let name2 = document.getElementById('addressToUpdate').value;
    let name3 = document.getElementById('phoneNumberToUpdate').value;
    let name4 = document.getElementById('taxNumberToUpdate').value;
    fetch('http://localhost:62480/animalShelter', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                shelterId: shelterIdToUpdate,
                shelterName: name,
                address: name2,
                phoneNumber: name3,
                taxNumber: name4
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function remove(id) {
    fetch('http://localhost:62480/animalShelter/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}