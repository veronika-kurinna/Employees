import { useEffect, useState } from 'react';
import './../Css/App.css';
import Papa from 'papaparse';
import { getEmployees, createEmployees, updateEmployee, deleteEmployee } from '../Scripts/httpClient';
import { phoneValidation } from '../Scripts/helpers';
import { FileInput } from './FileInput';

function App() {
    const [employees, setEmployees] = useState();
    const [rows, setRows] = useState();
    const [isFileInvalid, setIsFileInvalid] = useState();
    const [employeeName, setEmployeeName] = useState();
    const [dateOfBirth, setDateOfBirth] = useState();
    const [married, setMarried] = useState();
    const [phone, setPhone] = useState();
    const [salary, setSalary] = useState();

    const headers = [
        { title: 'Name', key: 'name', headerName: 'Name' },
        { title: 'Date Of Birth', key: 'dateOfBirth', headerName: 'DateOfBirth' },
        { title: 'Married', key: 'married', headerName: 'Married' },
        { title: 'Phone', key: 'phone', headerName: 'Phone' },
        { title: 'Salary', key: 'salary', headerName: 'Salary' }
      ];
    
    useEffect(() => {
        getEmployees()
            .then(employees => {
                setEmployees(mapEmployees(employees));
            });

        setIsFileInvalid(true);
    }, []);

    const dataFromDatabase = employees === undefined
        ? <p><em>Loading...</em></p>
        : <table id="table" className="table table-bordered">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Date Of Birth</th>
                    <th>Married</th>
                    <th>Phone</th>
                    <th>Salary</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                {employees.map(employee =>
                    <tr key={employee.id}>
                        {employee.isEditable ? <td><input type="text" className="form-control" defaultValue={employee.name} 
                            onChange={e => setEmployeeName(e.target.value)}/></td>: <td>{employee.name}</td>}
                        {employee.isEditable ? <td><input type="text" className="form-control" defaultValue={employee.dateOfBirth} 
                            onChange={e => setDateOfBirth(e.target.value)}/></td>: <td>{employee.dateOfBirth}</td>}
                        {employee.isEditable ? <td><input type="text" className="form-control" defaultValue={employee.married} 
                            onChange={e => setMarried(e.target.value)}/></td>: <td>{String(employee.married)}</td>}
                        {employee.isEditable ? <td><input type="text" className="form-control" defaultValue={employee.phone} 
                            onChange={e => setPhone(e.target.value)}/></td>: <td>{employee.phone}</td>}
                        {employee.isEditable ? <td><input type="text" className="form-control" defaultValue={employee.salary} 
                            onChange={e => setSalary(e.target.value)}/></td>: <td>{employee.salary}</td>}
                  
                        <td style={{width:"125px"}}> 
                            {employee.isEditable ?  
                            <div class="input-group">
                                <button class="btn btn-dark btn-sm" type="button" onClick={() => cancelEditClickHandler(employee)}>Cancel</button>
                                <button class="btn btn-primary btn-sm" type="button" onClick={() => saveClickHandler(employee)}>Save</button>
                            </div> :                             
                            <div>
                                <button className="btn btn-default btn-sm" type="button" onClick={() => isEditable(employee)}>
                                    <i className="bi bi-pencil-fill"></i>
                                </button>
                                <button className="btn btn-default btn-sm" type="button" onClick={() => deleteEmployee(employee.id)}>
                                    <i className="bi bi-trash-fill"></i>
                                </button>
                            </div>}
                        </td>
                    </tr>
                )}
            </tbody>
        </table>;

    const dataFromCSV = rows === undefined
        ? <p><em>No content...</em></p>
        : <table id="table" className="table table-hover text-center">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Date Of Birth</th>
                    <th>Married</th>
                    <th>Phone</th>
                    <th>Salary</th>
                </tr>
            </thead>
            <tbody>
                {rows.map(row =>
                    <tr key={row.name}>
                        <td className={row.name == undefined ? 'bg-danger' : ''}>{row.name}</td>
                        <td className={row.dateOfBirth == undefined || isNaN(Date.parse(row.dateOfBirth)) ? 'bg-danger' : ''}>{row.dateOfBirth}</td>
                        <td className={typeof row.married !== "boolean" ? 'bg-danger' : ''}>{String(row.married)}</td>
                        <td className={row.phone == undefined || phoneValidation(row.phone) == 'Incorrect phone number' ? 'bg-danger' : ''}>{row.phone}</td>
                        <td className={typeof row.salary !== "number" || row.salary == undefined ? 'bg-danger' : ''}>{row.salary}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 className='mt-3 mb-4'>Employees</h1>
            {dataFromDatabase}
            <FileInput function={onFileSelected}/>
            {dataFromCSV}
            <button className="btn btn-secondary mt-4 ms-4" onClick={() => createEmployees(rows)} disabled={isFileInvalid}>Upload CSV file</button>
        </div>
    );

    function mapEmployees(employees) {
        let employeeViews = employees;
        employees.forEach(employee => employee.IsEditable = false);
        return employeeViews;
    }

    function saveClickHandler(e){
        let employeeToUpdate = {
            id: e.id, 
            name: employeeName == undefined ? e.name : employeeName, 
            dateOfBirth: dateOfBirth == undefined ? e.dateOfBirth : dateOfBirth, 
            married: married == undefined ? e.married : married, 
            phone: phone == undefined ? e.phone : phone, 
            salary: salary == undefined ? e.salary : salary};

        const newEmployees = employees.map((employee) => {
            if (employee.id === e.id) {
              return employeeToUpdate;
            }
            return employee;
          });

        setEmployees(newEmployees);
        updateEmployee(employeeToUpdate);
    }

    function parseCSV (results) {
        if (results && results.data.length && results.meta.fields) {
            setIsFileInvalid(false);
            let fileHeaders = results.meta.fields;
            let missingHeaders = [];

            for (let i = 0; i < headers.length; i++) {
              if (
                !fileHeaders.some((fileHeader) => fileHeader == headers[i].headerName)
              ) {
                missingHeaders.push(headers[i].headerName);
              }
            }

            if (missingHeaders.length > 0) {
                setIsFileInvalid(true);
                for (let i = 0; i < missingHeaders.length; i++) {
                    console.log(`Column ${missingHeaders[i]} was missed`);
                }
            } else {
                setRows(results.data.map((currentRow) => {
                    if (currentRow['Name'] == undefined || currentRow['DateOfBirth'] == undefined || !['true', 'false'].includes(currentRow['Married']
                        .toString().toLowerCase()) || typeof currentRow['Phone'] == undefined || phoneValidation(currentRow['Phone']) == 'Incorrect phone number'
                        || typeof currentRow['Salary'] != 'number') 
                    {
                        setIsFileInvalid(true);
                    }
            
                    return {
                        name: currentRow['Name'],
                        dateOfBirth: currentRow['DateOfBirth'],
                        married: currentRow['Married'],
                        phone: currentRow['Phone'],
                        salary: currentRow['Salary'],
                    };
                }))
            }
        }
    };

    function handleFileUpload (file) {
        if (file) {
            Papa.parse(file, {
                complete: parseCSV,
                header: true,
                skipEmptyLines: true,
                dynamicTyping: true,
            });
        }
    };

    function onFileSelected (event) {
        const element = event.target;
        if (!element.files) return;
      
        const file = element.files[0];
        
        handleFileUpload(file);
    };

    function isEditable(e){
        const newEmployees = employees.map((employee) => {
            if (employee.id === e.id) {
              return {...employee, isEditable: true};
            }
            return employee;
          });
        
        setEmployees(newEmployees);
    }

    function cancelEditClickHandler(e){
        const newEmployees = employees.map((employee) => {
            if (employee.id === e.id) {
              return {...employee, isEditable: false};
            }
            return employee;
          });
        
        setEmployees(newEmployees);
    }
}

export default App;