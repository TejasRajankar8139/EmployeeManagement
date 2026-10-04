import { useEffect, useState } from "react";
import {getEmployees, deleteEmployee, getEmployeeById,  updateEmployee
} from "../services/employeeService";


function EmployeeTable({ setSelectedEmployee, refreshEmployees }) {

    const [name, setName] = useState("");
    const [employees, setEmployees] = useState([]);

    useEffect(() => {
    const loadEmployees = async () => {
        const data = await getEmployees();
        setEmployees(data);
    };

    loadEmployees();
}, [refreshEmployees]);
    const handleDelete = async (id) => {
    try {
        await deleteEmployee(id);

        // Remove the employee from the UI
        setEmployees((currentEmployees) =>
            currentEmployees.filter(employee => employee.id !== id)
        );

    } catch (error) {
        console.error("Failed to delete employee:", error);
    }
};

const handleEdit = async (id) => {
    try {
        const employee = await getEmployeeById(id);

        setSelectedEmployee(employee);

    } catch (error) {
        console.error("Failed to get employee:", error);
    }
};
return (
    <div>
        <h2>Employees</h2>

        <table className="table table-striped table-hover table-bordered">
            <thead className="table-dark">
                <tr>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Email</th>
                    <th>Department</th>
                    <th>Active</th>
                </tr>
            </thead>

            <tbody>
                {employees.map((employee) => (
                    <tr key={employee.id}>
                        <td>{employee.firstName}</td>
                        <td>{employee.lastName}</td>
                        <td>{employee.email}</td>
                        <td>{employee.department}</td>
                        <td>{employee.isActive ? "Yes" : "No"}</td>
                         <td>
                               <button
    className="btn btn-sm btn-warning me-2"
    onClick={() => handleEdit(employee.id)}
>
    Edit
</button>

<button
    className="btn btn-sm btn-danger"
    onClick={() => handleDelete(employee.id)}
>
    Delete
</button>
                            </td>

                    </tr>
                ))}
            </tbody>
        </table>
    </div>
);
    return (
        <div>
            <input
                type="text"
                value={name}
                onChange={(event) => setName(event.target.value)}
                placeholder="Enter employee name"
            />

            <h2>Hello {name}</h2>
        </div>
    );

    
}

export default EmployeeTable;

