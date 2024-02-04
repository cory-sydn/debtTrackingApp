import React, { useState } from 'react';
import styles from '../../styles/Admin.module.css';

export const AddNewAgent = ({ agents, setAgents }) => {
  const [agentInfo, setAgentInfo] = useState({ AgentName: "", Password: "", ProjectName: "", IsExecutive: false, IsActive: true, debtCallDetailsCount: 0 });
  const [message, setMessage] = useState(null);


  const handleAgentInfo = (e) => {
    setAgentInfo({ ...agentInfo, [e.target.name]: e.target.value });
    console.log(agentInfo)
  }
  
  const handleSaveAgent = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch('https://localhost:7160/api/Agents',
        {
          method: 'POST',
          headers: { 'Content-type': 'application/json' },
          body: JSON.stringify(agentInfo)
        }
      )

      const newAgent = await response.json();
      console.log("New Agent before setAgents: ", newAgent);

      if (!newAgent.id || !newAgent.agentName || !newAgent.projectName) {
        console.error("Invalid response data:", newAgent);
        return;
      }

      setAgents([...agents, newAgent]);
      setAgentInfo({ AgentName: "", Password: "", ProjectName: "", IsExecutive: false, IsActive: false });

      setMessage("Agent created successfully!");
    }
    catch (err) {
      console.log("Error : " + err)
    }
  }

  return (
    <div className={styles.formContainer} >
      <form className={styles.formStyle} action="api/Agents" encType="application/x-www-form-urlencoded" method="post">
        <label className={styles.formLabel} htmlFor="AgentName" >Agent Name And Surname </label>
        <input className={styles.formInput} type="text" placeholder="Enter Agent's Name And Surname" id="AgentName" name="AgentName" onChange={handleAgentInfo} value={agentInfo.AgentName} /><br />
        <label className={styles.formLabel} htmlFor="ProjectName" >Project </label>
        <input className={styles.formInput} type="text" placeholder="Enter Agent's projectName" name="ProjectName" onChange={handleAgentInfo} value={agentInfo.ProjectName} /><br />
        <label className={styles.formLabel} htmlFor="Password" >Password </label>
        <input className={styles.formInput} type="text" placeholder="Enter Agent's Password" name="Password" onChange={handleAgentInfo} value={agentInfo.Password} /><br />
        <p>
          <input type='checkbox' name="IsExecutive" defaultChecked={agentInfo.IsExecutive} />
          <label > &emsp; Executive </label><br />
        </p>
        <p>
          <input type='checkbox' name="IsActive" defaultChecked={agentInfo.IsActive} />
          <label > &emsp; Active </label><br /><br />
        </p>
        <p className={styles.massage}>
          {message}
        </p>
        <button className={styles.saveButton} onClick={handleSaveAgent} >Save</button>
        <br /><br />
      </form>
    </div>
  )
}