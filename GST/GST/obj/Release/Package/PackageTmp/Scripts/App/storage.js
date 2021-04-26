var StorageApp = {
    name: "localuser",
    Get(name) {
        let val = localStorage.getItem(name) || null;
        return val;
    },
    Set(name, val) {
        localStorage.setItem(name, val || null);
    },
    SetLocal(name, val) {
        debugger
        let localuser = this.Get(this.name) || {};
        localuser[name] = val;
        localStorage.setItem(this.name, val);
    },
    GetLocal(name) {
        let localuser = this.Get(this.name) || {};
        localuser[name] = localuser[name] || null;
    }
}