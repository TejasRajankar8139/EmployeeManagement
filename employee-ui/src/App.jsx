import { useState } from "react";
import EmployeeTable from "./components/EmployeeTable";
import EmployeeForm from "./components/EmployeeForm";

function App() {
    const [selectedEmployee, setSelectedEmployee] = useState(null);
    const [refreshEmployees, setRefreshEmployees] = useState(false);

    return (
        <div className="container mt-4">
    <h1 className="text-center mb-4">
        Employee Management System
    </h1>

    <EmployeeForm
        selectedEmployee={selectedEmployee}
        setSelectedEmployee={setSelectedEmployee}
        setRefreshEmployees={setRefreshEmployees}
    />

    <hr className="my-4" />

    <EmployeeTable
        setSelectedEmployee={setSelectedEmployee}
        refreshEmployees={refreshEmployees}
    />
</div>
    );
}

export default App;