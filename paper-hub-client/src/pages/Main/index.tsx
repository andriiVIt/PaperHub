import {NavLink} from "react-router-dom";
import {ROUTES} from "../../../utils";

function Main() {
    return (
        <div>
            <div>Paper Hub</div>
            <div>Select your role</div>
            <NavLink to={ROUTES.ADMIN}>
                Admin
            </NavLink>


            <NavLink to={ROUTES.CUSTOMER}>
                CUSTOMER
            </NavLink>
        </div>

    );
}

export default Main;