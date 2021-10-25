"""
contains code for reading inputs and any preprocessing necessary to make your algorithm work
"""
import psycopg2
import json

class ConnectAndQuery():

    def __init__(self, host, port, database, username, password):

        self.connect = psycopg2.connect(host=host, port=port, database=database, user=username, password=password)
        self.cur = self.connect.cursor()

    def getQueryPlan(self, query=None):
        
        self.queryPlan = ""
        
        if query:
            self.query = query
            try:
                self.cur.execute("EXPLAIN (ANALYZE, FORMAT JSON)" + self.query)
                plan = self.cur.fetchall()
                self.queryPlan = plan[0][0][0]["Plan"]

            except Exception as e:
                print ("\nError: %s" %str(e))
                self.connect.rollback()

        else:
            self.queryPlan = "Failed to generate query plan!"

        return json.dumps(self.queryPlan, sort_keys=False, indent=4)

if __name__ == '__main__':

    sample = 'SELECT * FROM customer WHERE c_custkey > 4 and c_custkey < 10'
    query = ConnectAndQuery('localhost', 'test', 'postgres', 'admin123')
    sample = query.getQueryPlan('SELECT * FROM customer WHERE c_custkey > 4 and c_custkey < 100')
    print(sample)

