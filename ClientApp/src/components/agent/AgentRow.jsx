import React, { useState } from 'react';
import styles from '../../styles/Admin.module.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faEye } from '@fortawesome/free-solid-svg-icons';
import { faEyeSlash } from '@fortawesome/free-solid-svg-icons';


export const AgentRow = ({ agent }) => {
  const [editable, setEditable] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const [agentInfo, setAgentInfo] = useState({
    Id: agent.id,
    AgentName: agent.agentName,
    Password: agent.password,
    ProjectName: agent.projectName,
    IsExecutive: agent.isExecutive,
    IsActive: agent.isActive   
  });
  const [message, setMessage] = useState("");


  const handleAgentInfo = (e) => {
    setAgentInfo({ ...agentInfo, [e.target.name]: e.target.value });
    console.log(agentInfo)
  }

  const handleChangeSubmit = async (e) => {
    e.preventDefault();    
    try {
      const response = await fetch('https://localhost:7160/api/Agents/' + agent.id,
        {
          method: 'PUT',
          headers: { 'Content-type': 'application/json' },
          body: JSON.stringify(agentInfo)
        }
      )

      const properCase = (str) => {
        return str.toLowerCase().split(' ').map(word =>  word.charAt(0).toUpperCase() + word.slice(1) ).join(' '); 
      }
      
      if (response.ok) {
        setEditable(false);
        setMessage(properCase(agent.agentName) + " updated successfullly ")
      }
    }
    catch (err) {
      console.log("Error : " + err)
    }    
  }

  return (
    <tr className={styles.trStyle} key={agent.id} onDoubleClick={() => setEditable(!editable)}>
      <td className={styles.tdStyle} > {agent.id} </td>
      <td className={styles.tdStyle} >
        <input className={styles.formInput}
          defaultValue={agent.agentName} type="text"
          placeholder="Enter Agent's Name And Surname"
          name="AgentName"
          onChange={handleAgentInfo}
          style={editable ? null : { borderBottom: '1px solid #0000' }}
          disabled={!editable}
        />
      </td>
      <td className={styles.tdStyle} >
        <input className={styles.formInput}
          type="text" placeholder="Enter Agent's projectName"
          name="ProjectName"
          onChange={handleAgentInfo}
          defaultValue={agent.projectName}
          style={editable ? null : { borderBottom: '1px solid #0000' }}
          disabled={!editable}
        />
      </td>
      <td className={styles.tdStyle} >
        <input className={styles.passwordInput}
          type={showPassword ? "text" : "password"}
          placeholder="Enter Agent's Password" name="Password"
          onChange={handleAgentInfo} defaultValue={agent.password}
          style={editable ? null : { borderBottom: '1px solid #0000' }}
          disabled={!editable}
        />
        {showPassword !== agent.id
          ?
          <FontAwesomeIcon className={styles.formIcon} icon={faEye} size="xs" onClick={() => setShowPassword(!showPassword)} />
          :
          <FontAwesomeIcon className={styles.formIcon} icon={faEyeSlash} size="xs" onClick={() => setShowPassword(!showPassword)} />
        }
      </td>
      <td className={styles.tdStyle} >
        <input className={styles.formCheckbox} type='checkbox' name="isExecutive" defaultChecked={agent.isExecutive} disabled={!editable} />
      </td>
      <td className={styles.tdStyle} >
        <input className={styles.formCheckbox} type='checkbox' name="isActive" defaultChecked={agent.isActive} disabled={!editable} />
      </td>
      <td className={true ? styles.tdStyle : "unvisible"} >
        <button className={`${styles.saveButton} ${styles.saveButtonXS}`}
          type='submit'
          data-agentid={agent.id}
          onClick={handleChangeSubmit}
          disabled={!editable}
          style={editable ? null : { pointerEvents: "none", opacity: "50%" }}
        >
          <p className={styles.massage}>
            {message}
          </p>
          Save Changes
        </button>
      </td>
    </tr>
  )
}
