import React, { useState, useEffect } from 'react';
import styles from '../styles/Admin.module.css';
import { AgentRow } from '../components/agent/AgentRow';
import { AddNewAgent } from '../components/agent/AddNewAgent';

const Admin = () => {
  const [open, setOpen] = useState(false);
  const [agents, setAgents] = useState([]);

  useEffect(() => {
    const abortController = new AbortController();

    const fetchAgents = async () => {
      try {
        const response = await fetch('https://localhost:7160/api/agents',
          { signal: abortController.signal },
          {
            headers: {
              'Content-Type': 'application/json',
              'Method': 'GET',
              'Accept': 'application/json'
            }
          }
        );
        const data = await response.json();       
        setAgents(data);

      } catch (err) {
        console.log("Error : " + err)
      }
    }

    fetchAgents();

    return () => {
      abortController.abort();
    };
  }, [])


  const getAgentWithCount = (result) => {
    const newAgent = { ...result }; // Copy the original object
    newAgent.debtCallDetailsCount = result.debtCallDetails.length;
    delete newAgent.debtCallDetails; // Remove the original array
    return newAgent;
  };


  return (
    <div className={styles.pageContainer}>
      <h3 style={{ paddingBottom: 16, paddingTop: 32 }} >{agents != null ? "Agents" : ""}</h3>
      <button
        className={styles.addButton}
        onClick={() => setOpen(!open)}
        title="Add New Agent"
        type="button"
      >Add New Agent
      </button>
      {open
        ? <AddNewAgent setOpen={setOpen} agents={agents} setAgents = { setAgents } />
        : null
      }
      {agents != null
        ?
        (<table cellSpacing='16' cellPadding='8' className={styles.tableStyle}>
          <colgroup span="16"></colgroup>
          <thead>
            <tr className={styles.tableHeader} >
              <th >ID</th>
              <th >Agent Name</th>
              <th >Project</th>
              <th >Password</th>
              <th >Is Executive</th>
              <th >Is Active</th>
              <th className={true ? styles.tdStyle : "unvisible"} >&emsp;</th>
            </tr>
          </thead>
          <tbody>
            {(agents.map(agent =>                
              <AgentRow
                agent={agent}                  
              />                
            ))}
          </tbody>
        </table>)
        :
        <p>Loading...</p>
      }
    </div>
  )
}
export default Admin;