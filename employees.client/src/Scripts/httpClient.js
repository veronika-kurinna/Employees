const baseUrl = "https://localhost:7101";

export const getEmployees = async() => {
    let request = {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    };

   return fetch(`${baseUrl}/api/Employee/Get`, request)
        .then(response => response.json())
        .then(response => response.employees)
        .catch(error => console.log(error.message));
};

export const createEmployees = async(rows) => {
    let employees = {"employees": rows}

    let request = {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(employees)
    };

   return fetch(`${baseUrl}/api/Employee/CreateEmployees`, request)
        .catch(error => console.log(error.message));
};

export const updateEmployee = (employee) => {
    let request = {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(employee)
    }

    return fetch(`${baseUrl}/api/Employee/Update/${employee.id}`, request)
        .catch(error => console.log(error.message));
}

export const deleteEmployee = (id) => {
    let request = {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(id)
    }

    return fetch(`${baseUrl}/api/Employee/Delete/${id}`, request)
        .catch(error => console.log(error.message));
}