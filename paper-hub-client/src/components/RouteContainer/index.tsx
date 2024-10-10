import  { FC, Suspense } from 'react'
import { Routes, Route  } from 'react-router-dom'
import {ROUTES} from "../../../utils";
import Main from "../../pages/Main";
import AdminDashboard from "../../pages/Admin/AdminDashboard";
import CustomerSelection from "../../pages/Customer/CustomerSelection";
import DashboardLayoutSlots from "../../pages/Customer/CustomerWindow";








// const Main = lazy(() => import('pages/Main'))


export const RouteContainer: FC = () => {
  return (
    <Suspense fallback={<div>Loading----((((</div>}>
      <Routes>
        <Route path={ROUTES.MAIN} element={<Main/>}/>
        <Route path={ROUTES.ADMIN} element={<AdminDashboard/>}/>
        <Route path={ROUTES.CUSTOMER} element={<CustomerSelection/>}/>
          <Route path={ROUTES.SINGLE_CUSTOMER} element={<DashboardLayoutSlots/>}/>




      </Routes>
    </Suspense>
  )
}
