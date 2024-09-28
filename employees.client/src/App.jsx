import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [employees, setEmployees] = useState();
    const baseUrl = "https://localhost:7101";

    useEffect(() => {
        getEmployees()
            .then(items => {
                setEmployees(items);
                console.log(items);
            }); 
    }, []);

    const contents = employees === undefined
        ? <p><em>Loading...</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Name</th>
                    <th>Date Of Birth</th>
                    <th>Married</th>
                    <th>Phone</th>
                    <th>Salary</th>
                </tr>
            </thead>
            <tbody>
                {employees.map(employee =>
                    <tr key={employee.id}>
                        <td>{employee.id}</td>
                        <td>{employee.name}</td>
                        <td>{employee.dateOfBirth}</td>
                        <td>{String(employee.married)}</td>
                        <td>{employee.phone}</td>
                        <td>{employee.salary}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tableLabel">Employees</h1>
            {contents}
        </div>
    );

    async function getEmployees() {
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
    }
}

export default App;