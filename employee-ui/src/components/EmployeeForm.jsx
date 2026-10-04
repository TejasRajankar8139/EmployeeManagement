import {
    createEmployee,
    updateEmployee
} from "../services/employeeService";

import { useEffect, useState } from "react";

function EmployeeForm({
    selectedEmployee,
    setSelectedEmployee,
    setRefreshEmployees
}) {

    const [employee, setEmployee] = useState({
        firstName: "",
        lastName: "",
        email: "",
        department: "",
        dateOfJoining: ""
    });
useEffect(() => {
    if (selectedEmployee) {
        setEmployee({
            firstName: selectedEmployee.firstName,
            lastName: selectedEmployee.lastName,
            email: selectedEmployee.email,
            department: selectedEmployee.department,
            dateOfJoining: selectedEmployee.dateOfJoining
                ? selectedEmployee.dateOfJoining.substring(0, 10)
                : ""
        });
    }
}, [selectedEmployee]);
    const handleChange = (event) => {
        const { name, value } = event.target;

        setEmployee({
            ...employee,
            [name]: value
        });
    };
// const handleSubmit = async (event) => {
//     event.preventDefault();

//     const createdEmployee = await createEmployee(employee);

//     console.log(createdEmployee);
// };
const handleSubmit = async (event) => {
    event.preventDefault();

    try {
        if (selectedEmployee) {

            // EDIT
            const updatedEmployee = await updateEmployee(
            selectedEmployee.id,
            {
                ...employee,
                 isActive: selectedEmployee.isActive
            }
        );

console.log("Updated employee:", updatedEmployee);

setRefreshEmployees(prev => !prev);

            console.log("Updated employee:", updatedEmployee);

        } else {

            // CREATE
            const createdEmployee = await createEmployee(employee); 
            console.log("Created employee:", createdEmployee);
            setRefreshEmployees(prev => !prev);
        }

    } catch (error) {
        console.error("Failed to save employee:", error);
    }
};
   return (
    <div className="container mt-4">
        <div className="card shadow-sm">
            <div className="card-body">

                <h2 className="card-title mb-4">
                    {selectedEmployee ? "Update Employee" : "Add Employee"}
                </h2>

                <div className="row">

                    {/* First Name */}
                    <div className="col-md-6 mb-3">
                        <label className="form-label">First Name</label>
                        <input
                            className="form-control"
                            type="text"
                            name="firstName"
                            placeholder="Enter first name"
                            value={employee.firstName}
                            onChange={handleChange}
                        />
                    </div>

                    {/* Last Name */}
                    <div className="col-md-6 mb-3">
                        <label className="form-label">Last Name</label>
                        <input
                            className="form-control"
                            type="text"
                            name="lastName"
                            placeholder="Enter last name"
                            value={employee.lastName}
                            onChange={handleChange}
                        />
                    </div>

                    {/* Email */}
                    <div className="col-md-6 mb-3">
                        <label className="form-label">Email</label>
                        <input
                            className="form-control"
                            type="email"
                            name="email"
                            placeholder="Enter email"
                            value={employee.email}
                            onChange={handleChange}
                        />
                    </div>

                    {/* Department */}
                    <div className="col-md-6 mb-3">
                        <label className="form-label">Department</label>
                        <input
                            className="form-control"
                            type="text"
                            name="department"
                            placeholder="Enter department"
                            value={employee.department}
                            onChange={handleChange}
                        />
                    </div>

                    {/* Date of Joining */}
                    <div className="col-md-6 mb-3">
                        <label className="form-label">Date of Joining</label>
                        <input
                            className="form-control"
                            type="date"
                            name="dateOfJoining"
                            value={employee.dateOfJoining}
                            onChange={handleChange}
                        />
                    </div>

                </div>

                {/* Submit Button */}
                <button
                    className={`btn ${
                        selectedEmployee ? "btn-warning" : "btn-primary"
                    }`}
                    onClick={handleSubmit}
                >
                    {selectedEmployee ? "Update Employee" : "Add Employee"}
                </button>

                {/* Cancel Edit */}
                {selectedEmployee && (
                    <button
                        className="btn btn-secondary ms-2"
                        onClick={() => setSelectedEmployee(null)}
                    >
                        Cancel
                    </button>
                )}

            </div>
        </div>
    </div>
);
}

export default EmployeeForm;