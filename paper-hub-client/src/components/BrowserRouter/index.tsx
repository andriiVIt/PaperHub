import { useLayoutEffect, useRef, useState } from 'react'
import { BrowserHistory } from 'history'
import { BrowserRouterProps, Router } from 'react-router-dom'
import {customHistory} from "../../../utils";



export function BrowserRouter(props: BrowserRouterProps) {
  const historyRef = useRef<BrowserHistory | null>(null);
  if (historyRef.current === null) {
    historyRef.current = customHistory;
  }
  const history = historyRef.current;
  const [state, setState] = useState({
    action: history.action,
    location: history.location
  });

  useLayoutEffect(() => history.listen(setState), [history]);

  return (
    <Router
      {...props}

      location={state.location}
      navigationType={state.action}
      navigator={history}
    />
  );
}
