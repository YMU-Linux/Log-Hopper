// Sample data generator
function makeSampleData(count = 20) {
  const data = [];
  for (let i = 1; i <= count; i++) {
    data.push({
      id: i,
      name: 'User ' + i,
      email: 'user' + i + '@example.com',
      created: new Date(Date.now() - i * 86400000).toLocaleDateString()
    });
  }
  return data;
}

// Build and insert table into #tableContainer
function renderTable(containerId, data, columns) {
  const container = document.getElementById(containerId);
  container.innerHTML = ''; // clear

  const wrapper = document.createElement('div');
  wrapper.className = 'table-responsive';

  const table = document.createElement('table');
  table.className = 'table table-striped table-hover table-sm mb-0';

  // Build head
  const thead = document.createElement('thead');
  const theadRow = document.createElement('tr');
  columns.forEach(col => {
    const th = document.createElement('th');
    th.scope = 'col';
    th.textContent = col.label;
    theadRow.appendChild(th);
  });
  thead.appendChild(theadRow);
  table.appendChild(thead);

  // Build body
  const tbody = document.createElement('tbody');
  data.forEach(row => {
    const tr = document.createElement('tr');
    columns.forEach(col => {
      const td = document.createElement('td');
      td.textContent = (typeof col.key === 'function') ? col.key(row) : row[col.key];
      tr.appendChild(td);
    });
    tbody.appendChild(tr);
  });
  table.appendChild(tbody);

  wrapper.appendChild(table);
  container.appendChild(wrapper);
}

// Initial setup and buttons
(function init() {
  let data = makeSampleData(22);
  const columns = [
    { label: '#', key: 'id' },
    { label: 'Name', key: 'name' },
    { label: 'Email', key: 'email' },
    { label: 'Created', key: 'created' }
  ];

  const render = () => renderTable('tableContainer', data, columns);

  // initial render
  render();

  // Refresh button: regenerate sample data
  document.getElementById('refreshBtn').addEventListener('click', () => {
    data = makeSampleData(22); // new data
    render();
  });

  // Add row button: push a new random row
  document.getElementById('addRowBtn').addEventListener('click', () => {
    const nextId = data.length ? Math.max(...data.map(d => d.id)) + 1 : 1;
    data.push({
      id: nextId,
      name: 'New User ' + nextId,
      email: 'new' + nextId + '@example.com',
      created: new Date().toLocaleDateString()
    });
    render();

    // Auto-scroll to bottom to see the appended row
    const container = document.getElementById('tableContainer');
    container.scrollTop = container.scrollHeight;
  });
})();
